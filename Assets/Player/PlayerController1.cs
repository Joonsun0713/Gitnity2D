using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{

    Rigidbody2D rb;
    PlayerJump PJump;
    float Hz;

    [SerializeField]
    float JumpPower = 5.0f; // 점프값 
    public bool JumpA = false;  //점프 판단
    [SerializeField]
    float MoveSpeed = 5.0f; //이동 속도
    [SerializeField]
    
    public int PlayerLife = 5;
  
   
    //bool Playerfilp = false;  // 플레이어 좌우반전

    public Transform PlayerPos;
    public Vector2 bSize;

    float AtcurTime = 0.0f;
    public float AttackCoolTime = 1.5f;

    Animator animator;
   
    

      //현재 애니메이션 상태


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // 플레이어 오브젝트가 회전하지 않게 하기 
        PJump = GetComponentInChildren<PlayerJump>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        AtcurTime += Time.deltaTime;
       
        Hz = Input.GetAxisRaw("Horizontal"); //이동키 값 받기

        if (Hz == -1)
        {

            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("Move", true);

        }
        else if (Hz == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("Move", true);
        }
        else if( Hz==0)
        {
            animator.SetBool("Move", false);
        }


        // 플레이어 공격

        if (Input.GetMouseButtonDown(0)) //마우스 좌클릭을 했을 때
        {
            //공격

            if (AtcurTime > AttackCoolTime)
            {
                animator.SetTrigger("Attack");
                Attack();
                AtcurTime = 0.0f;
                
            }
            else
            {
                Debug.Log("아직 쿨타임이 안지났습니다."+ AtcurTime);
            }

        }



    }
        void Attack()
        {


             Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(PlayerPos.position, bSize, 0);
             foreach (Collider2D collider in collider2Ds)
             {
                if (collider.tag == "enemy")
                {
                   Debug.Log("공격");
                }




             }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 7)
            {
                PlayerLife -= 1;
                Debug.Log("현재 HP = " + PlayerLife);

               rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(PlayerPos.position, bSize);
        }


        void FixedUpdate()
        {
           
            rb.velocity = new Vector2(Hz * MoveSpeed, rb.velocity.y);  // 이동 값

            if (JumpA)
            {
                Debug.Log("점프 실행중");
                rb.velocity = new Vector2(rb.velocity.x, JumpPower);

                JumpA = false;

             
            }

        }
        
   
}
