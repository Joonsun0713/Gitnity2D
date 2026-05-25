using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

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

    bool isShield = false;
    bool isRoll = false;
    public int PlayerLife = 5;

    float RollCurTime = 0.0f;
    [SerializeField]
    float RollCoolTime = 2.5f;
    [SerializeField]
    float RollSpeed = 1.0f;

    //현재 애니메이션 상태


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PJump = GetComponentInChildren<PlayerJump>();
        ani = GetComponentInChildren<PlayerAnimator>();

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
            if (RollCurTime > RollCoolTime)
            {

                ani.PlayerRollAnimation();
                isRoll = true;
                Debug.Log("구르기 시작: Invincible = " + isRoll);
                RollSpeed = 1.5f;
                Invoke("MovingRollSpeed", 0.7f);
                RollCurTime = 0.0f;
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


        /*if (rb.velocity.y < -0.01f)
        {
            Debug.Log("y축 값" + Mathf.FloorToInt(rb.velocity.y));
            OnPlayerJumpFall(true);
        }
        else if (rb.velocity.y >= 0.0f)
        {
            //Debug.Log("y축 값" + rb.velocity.y);
            OnPlayerJumpFall(false);
        }*/
        
        if (PJump.onGround == false && rb.velocity.y < 0.0f)
        {
             OnPlayerJumpFall(true);
        }
        else if (PJump.onGround == true)
        {
                
             OnPlayerJumpFall(false);
        }
        
    }
    void MovingRollSpeed()  // Invoke()를 이용한 구르기 스피드 조정
    {
        RollSpeed = 1.0f;
        isRoll = false;
        Debug.Log("구르기 끝: Invincible = " + isRoll);
        Debug.Log("스피드 원상태");
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
            }

        }
        else if(isRoll || isShield) 
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

    }

 }
