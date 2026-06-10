using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController1 : MonoBehaviour
{
    Rigidbody2D rb; 
    PlayerJump PJump;
    PlayerAnimator ani;
    //이동 속도 변수
    float Hz;   //키보드 좌우 입력값 받기 위한 저장 -1~1
    [SerializeField]
    float JumpPower = 5.0f; // 점프값 수치
    [SerializeField]
    float MoveSpeed = 5.0f; //이동 속도
    [SerializeField]
    float RollSpeed = 1;    //구르기 속도 평소 1, 구르기 시 상승

    // 상태 판단
    public static bool JumpA = false;  //점프 가능 신호
    bool CanRoll = true;    // 구르기 가능 판단
    public static bool isShield = false;    // 쉴드 상태인지 판단하기 위한 변수
    bool isRoll = false;
    public static bool IsDead = false;  // 플레이어 사망 판정
    bool isHurt = false;
    float ShieldStamina = 0.0f; // 쉴드 작동 시 스태미나 누적 계산

    public static int PlayerLife = 100; // 캐릭터 체력
    public static int Stamina = 100;    // 캐릭터 스태미너

    public float recoverDelay = 0.1f; // 공격 후 회복 대기 시간
    float lastActionTime = 0.0f;

    int ST_Recover = 51;

    public Image ST_Image;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PJump = GetComponentInChildren<PlayerJump>();
        ani = GetComponent<PlayerAnimator>();

        PlayerLife = 100; // 캐릭터 체력 초기화
        Stamina = 100;    // 캐릭터 스태미너 초기화
        lastActionTime = Time.time;

}

    void Update()
    {
        if(IsDead) return;
       
        Hz = Input.GetAxisRaw("Horizontal"); //이동키 값 받기

        if (isShield)
        {
            ShieldStamina += Time.deltaTime;
            if (ShieldStamina > 1.0f)
            {
                UseStamina(10);
                ShieldStamina = 0.0f;
            }

            Hz = 0;
            
        }
        else
        {
            if (Hz == 1)    // 오른쪽 이동
            {

                transform.localScale = new Vector3(1, 1, 1);
                ani.SetMoveAnimation(true);
                //Debug.Log("애니메이션 작동 여부");

            }
            else if (Hz == -1) //왼쪽 이동
            {
                transform.localScale = new Vector3(-1, 1, 1);   //의미는 좌표 평면 대칭 이동 원리처럼
                ani.SetMoveAnimation(true);                     //모든 점의 x 좌표값이 정반대로 이동하면서 반대 방향을 바라노는 것처럼 보인다.

            }
            else if (Hz == 0)
            {
                ani.SetMoveAnimation(false);
            }
        }

       
        // 구르기 감지
        if (Input.GetKeyDown(KeyCode.LeftShift))    // 왼쪽 쉬프트 구르기 사용
        {
            if (CanRoll && Stamina > 0)    // 스태미너가 0 보다 많을 때 구르기 사용 CanRoll && 
            {
                StartCoroutine(RollRoutine());
            }
            
        }

        // 방패 감지
        if (Input.GetKey(KeyCode.E))
        {
            
            if (Stamina > 0)
            {
                ani.PlayerShieldAnimation(true);
                isShield = true;
            }
            else
            {
                Debug.Log("방패 스태미나 부족");
                ani.PlayerShieldAnimation(false);
                isShield = false;
            }

        }
        else
        {
            ani.PlayerShieldAnimation(false);
            isShield = false;
        }


        
        if (PJump.onGround == false && rb.velocity.y < 0.0f)    //onGround가 false이고 rb.velocity.y가 음수(낙하 중)일 때 낙하 애니메이션 켜기
        {
             OnPlayerJumpFall(true);
           
        }
        else if (PJump.onGround == true)                        // OnGround가 true(땅에 착지)일 때 낙하 애니메이션 끄기 
        {
                
             OnPlayerJumpFall(false);
        }
        
    }

    IEnumerator RollRoutine()   // 구르기 코루틴 
    {
        CanRoll = false;    // CanRoll false로 하여 쿨타임 전에 구르기 방지
        UseStamina(20); // 구르기 스태미너 소모 (20으로 설정 예시)
        ani.PlayerRollAnimation();  // 구르기 애니메이션 실행
        isRoll = true;  // 플레이어 무적 상태 만들기
        
        RollSpeed = 1.5f;   // 구르기 사용시 이동속도 높이기
        yield return new WaitForSeconds(0.7f);  // 코루틴에서 yield return new WaitForSeconds(float);는 매개변수로 입력한 숫자에 해당하는
                                                // 초만큼 기다렸다가 다음 명령어 수행

        RollSpeed = 1.0f;   // 0.7초 지난후(구르기 끝난 후) 원래 속력으로 돌아가기
        isRoll = false;     // 무적 상태 해제 

        yield return new WaitForSeconds(1.5f);  // 구르기 쿨타임 1.5초 시작
        CanRoll = true; //1.5초가 지나면 다시 구르기 사용 가능하게 CanRoll = true로 변경
        Debug.Log("1.5초 경과");
    }
   
    void RecoverStamina()
    {

            if (Stamina < 100)
            {
                // 일정 시간마다 스태미너 회복
                Stamina += (int)Time.deltaTime;
                if (Stamina > 100) Stamina = 100;

                // 여기서 UI를 즉시 갱신
                UpdateStaminaUI();
            }
        Debug.Log("스태미너 회복 시도 중, 현재 값: " + Stamina + " / 마지막 행동 시간차: " + (Time.time - lastActionTime));
    }

    public void Damage(int Hit) //피격 처리
    {
        if (PlayerLife <= 0) return;
        if (isHurt) return;
        if (!isShield && !isRoll)   //무적기 상태인 isShield와 isRoll가 모두 거짓일 때 히트 메소드 실행
        {
            StartCoroutine(HurtRoutine(Hit));

        }
        else if(isShield)  // 쉴드로 방어 시 효과 애니메이션 실행 *수정 예정*
        {
            ani.PlayerIsShieldAnimation();
            Debug.Log("방어 성공");
        }
    }
    
    IEnumerator HurtRoutine(int Hit)    // 코루틴 활용하여 공격 당할 때마다 쿨타임 주기
    {
        isHurt = true;
        PlayerLife -= Hit;  // 
        Debug.Log("현재 HP = " + PlayerLife);


        if (PlayerLife > 0)
        {
            
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);  //AddForce(방향* 힘 값, 힘의 종류), ForceMode2D.Impulse는 짦은 순간의 힘을 의미한다.
        }
        else            //PlayerLife가 0이하일 시 사망 애니메이션 실행
        {

            Die();
        }

        yield return new WaitForSeconds(1.5f);    // 1.5초 실행 
        isHurt = false;
    }

    public void OnPlayerComboAttack(int ComboStep) // 콤보공격 효과
    {
        ani.PlayComboAttackAnimation(ComboStep);
    }

    /*public void OnPlayerJumpUp() //점프 효과
    {
        ani.PlayerJumpUpAnimation();
    }*/

    public void OnPlayerJumpFall(bool IsFall) //낙하
    {
            ani.PlayerJumpFallAnimation(IsFall);
            //Debug.Log("착지 애니메이션");

    }

    void UpdateStaminaUI()
    {
        if (ST_Image != null)
        {
            float fill = (float)Stamina / 100f;
                ST_Image.fillAmount = fill;
       
        }
        else
        {
            Debug.LogError("ST_Image가 연결되지 않았습니다!");
        }
    }

    public void UseStamina(int amount)  // 구르기시 소모 스태미너
    {
        Stamina -= amount;

        if(Stamina < 0) Stamina = 0;
        lastActionTime = Time.time;     // 행동 시간 갱신
        UpdateStaminaUI();
    }

    void Die()  // 플레이어 사망 메서드
    {
        if(IsDead) return;
        IsDead = true;
        ani.PlayerDeathAnimation();
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;

    }

    void FixedUpdate() //물리 움직임
    {

        rb.velocity = new Vector2(Hz * MoveSpeed * RollSpeed, rb.velocity.y);  // X= (방향* 기본 속도 * 구르기 속도)

        if (JumpA)  //점프 실행
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
            ani.PlayerJumpUpAnimation();
            JumpA = false;
        }

        // --- 스태미너 자동 회복 로직 추가 ---
        // 마지막 행동 후 0.1초가 지났다면 회복 시작
        if (Time.time - lastActionTime > recoverDelay)
        {
            if (Stamina < 100)
            {
                // 회복량 대폭 향상 (초당 약 80 회복)
                Stamina += (int)(ST_Recover * Time.deltaTime);
                if (Stamina > 100) Stamina = 100;

                // UI 갱신
                UpdateStaminaUI();
            }
        }

    }
 }
