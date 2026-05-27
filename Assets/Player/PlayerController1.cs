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
    float Hz;

    [SerializeField]
    float JumpPower = 5.0f; // 점프값 
    public bool JumpA = false;  //점프 판단
    [SerializeField]
    float MoveSpeed = 5.0f; //이동 속도
    [SerializeField]

    float RollSpeed = 1;

    bool isShield = false;
    bool isRoll = false;

    public static int PlayerLife = 100; // 캐릭터 체력
    public static int Stamina = 100;    // 캐릭터 스태미너

    public float recoverDelay = 0.1f; // 공격 후 회복 대기 시간
    float lastActionTime = 0.0f;

    int ST_Recover = 80;

   
    public Image ST_Image;

    float RollCurTime = 0.0f;
    [SerializeField]
    float RollCoolTime = 2.5f;


    //bool Playerfilp = false;  // 플레이어 좌우반전

    //Animator animator;

    //현재 애니메이션 상태


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PJump = GetComponentInChildren<PlayerJump>();
        ani = GetComponentInChildren<PlayerAnimator>();

        PlayerLife = 100; // 캐릭터 체력 초기화
        Stamina = 100;    // 캐릭터 스태미너 초기화
        lastActionTime = Time.time;

}

    void Update()
    {

        RollCurTime += Time.deltaTime;
        Hz = Input.GetAxisRaw("Horizontal"); //이동키 값 받기
     
        if (Hz == 1)
        {

            transform.localScale = new Vector3(1, 1, 1);
            ani.SetMoveAnimation(true);
            //Debug.Log("애니메이션 작동 여부");

        }
        else if (Hz == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            ani.SetMoveAnimation(true);
        }
        else if (Hz == 0)
        {
            ani.SetMoveAnimation(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (RollCurTime > RollCoolTime && Stamina > 0)    // 스태미너가 0 보다 많을 때 구르기 사용
            {
                UseStamina(20); // 구르기 스태미너 소모 (20으로 설정 예시)
                ani.PlayerRollAnimation();
                isRoll = true;
                Debug.Log("구르기 시작: Invincible = " + isRoll);
                 RollSpeed = 1.5f;
                Invoke("MovingRollSpeed", 0.7f);
                RollCurTime = 0.0f;
            }
            else if (Stamina <= 0)
            {
                Debug.Log("스태미너가 부족하여 구를 수 없습니다!");
            }
            else
            {
                Debug.Log("아직 쿨타임이 안지났습니다." + RollCurTime);
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            ani.PlayerShieldAnimation(true);
            isShield = true;
        }
        else
        {
            ani.PlayerShieldAnimation(false);
            isShield = false;
        }

    }
    void MovingRollSpeed()  // Invoke()를 이용한 구르기 스피드 조정
    {
         RollSpeed = 1.0f;
        isRoll = false;
        Debug.Log("구르기 끝: Invincible = " + isRoll);
        
    }

    public void Damage(int Hit)
    {
        if (PlayerLife <= 0) return;

        if (!isShield && !isRoll)
        {
            PlayerLife -= Hit;
            Debug.Log("현재 HP = " + PlayerLife);


            if (PlayerLife > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            }
            else
            {
                rb.velocity = Vector2.zero;
                ani.PlayerDeathAnimation();
                PlayerDeath.isGameOver = true;
            }

        }
        else if(isShield) 
        {
            ani.PlayerIsShieldAnimation();
            Debug.Log("방어 성공");
        }
    }


    public void OnPlayerComboAttack(int ComboStep)
    {
        ani.PlayComboAttackAnimation(ComboStep);
    }

    public void OnPlayerJumpUp()
    {
        ani.PlayerJumpUpAnimation();
    }

    public void OnPlayerJumpFall(bool IsFall)
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

    void FixedUpdate()
    {

        rb.velocity = new Vector2(Hz * MoveSpeed * RollSpeed, rb.velocity.y);  // 이동 값

        if (JumpA)
        {
            //Debug.Log("점프 실행중");
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
            OnPlayerJumpUp();
            JumpA = false;
          
            
        }

        if (Stamina < 100 && Time.time - lastActionTime > recoverDelay)
        {
            Debug.Log("스태미너 회복 시도! 현재 값: " + Stamina); // 이 로그가 찍히는지 확인
            Stamina += (int)(ST_Recover * Time.deltaTime);
            if (Stamina > 100) Stamina = 100;

            UpdateStaminaUI();
        }

    }

 }
