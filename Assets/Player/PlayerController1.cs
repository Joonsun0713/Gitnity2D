using System;
using System.Collections;
using System.Collections.Generic;

//using System.Diagnostics;
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

    public int PlayerLife = 5;

    float RollCurTime = 0.0f;
    float RollCoolTime = 2.5f;

    //bool Playerfilp = false;  // 플레이어 좌우반전

    //Animator animator;

    //현재 애니메이션 상태


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // 플레이어 오브젝트가 회전하지 않게 하기 
        PJump = GetComponentInChildren<PlayerJump>();
        ani = GetComponentInChildren<PlayerAnimator>();

    }

    void Update()
    {
        RollCurTime += Time.deltaTime;

        Hz = Input.GetAxisRaw("Horizontal"); //이동키 값 받기

        if (Hz == -1)
        {

            transform.localScale = new Vector3(-1, 1, 1);
            ani.SetMoveAnimation(true);
            Debug.Log("애니메이션 작동 여부");

        }
        else if (Hz == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
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
        }
        else
        {
            ani.PlayerShieldAnimation(false);
        }
    }

        // 플레이어 공격



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
             PlayerLife -= 1;
             Debug.Log("현재 HP = " + PlayerLife);

             rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
    }


    public void OnPlayerAttack()
    {
        ani.PlayAttackAnimation();
    }

    public void OnPlayerJumpUp()
    {
        ani.PlayerJumpUpAnimation();
    }

    public void OnPlayerJumpFall(bool IsFall)
    {
            ani.PlayerJumpFallAnimation(IsFall);
            Debug.Log("착지 애니메이션");

    }

    void FixedUpdate()
    {

        rb.velocity = new Vector2(Hz * MoveSpeed, rb.velocity.y);  // 이동 값

        if (JumpA)
        {
            Debug.Log("점프 실행중");
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
            OnPlayerJumpUp();
            JumpA = false;
           /* if (rb.velocity.y < 0.0f)
            {
                Debug.Log("착지 중");
                OnPlayerJumpFall(true);
            }
            else if (rb.velocity.y >= 0.0f)
            {
                OnPlayerJumpFall(false);
            }*/
            
        }

        if (rb.velocity.y < 0.0f)
        {
            Debug.Log("착지 중");
            OnPlayerJumpFall(true);
        }
        else if (rb.velocity.y >= 0.0f)
        {
            OnPlayerJumpFall(false);
        }
    }

 }
