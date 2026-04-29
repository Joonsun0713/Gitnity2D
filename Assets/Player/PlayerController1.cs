using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    
    Rigidbody2D rb;
    float Hz;
    [SerializeField]
    float JumpPower = 5.0f; // 점프값 
    bool JumpA = false;  //점프 판단
    [SerializeField]
    float MoveSpeed = 5.0f; //이동 속도
    [SerializeField]
    int JumpCounter = 0; // 점프 횟수 측정

    bool onGround = false;  //착지 판정
    bool Playerfilp = false;  // 플레이어 좌우반전
    // 애니메이션 관련 
    public string StandardAnime = "";
    public string MovingAnime = "";
    public string JumpAnime = "";
    public string DownAnime = "";
    public string RollAnime = "";

    string nowMode = "";    //현재 애니메이션 상태


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // 플레이어 오브젝트가 회전하지 않게 하기 
    }

    void Update()
    {
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), 1 << 6);
        if (onGround) // 착지하면 점프 횟수 초기화
        {
            JumpCounter = 0;
        }
        Hz = Input.GetAxisRaw("Horizontal"); //이동키 값 받기

        if (Hz == -1)
        {
            Playerfilp = true;
            nowMode = MovingAnime;
        }
        else if (Hz == 1)
        {
            Playerfilp = false;
            nowMode = MovingAnime;
        }

        if (Hz == 0)
        {
            nowMode = StandardAnime;
        }

        if (rb.velocity.y > 0)
        {
            nowMode = JumpAnime;
        }
        else if(rb.velocity.y < 0)
        {
            nowMode = DownAnime;
        }

         if (Input.GetKeyDown(KeyCode.LeftShift))
         {
             nowMode = RollAnime;
         }





        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround || JumpCounter < 1) // 착지해 있거나, 점프 횟수가 충분하면 점프 가능
            {
                UnityEngine.Debug.Log("현재의 JumpCounter:" + JumpCounter);
                JumpA = true;
                ++JumpCounter;

            }

        }
        /*if (onGround) // 착지하면 점프 횟수 초기화
        {
            JumpCounter = 0;
        }*/
       // UnityEngine.Debug.Log("현재의 JumpCounter:"+JumpCounter);
        //onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.2f), 1 << 6);
    }
    void FixedUpdate()
    {
       // onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.2f), 1 << 6);
        rb.velocity = new Vector2(Hz * MoveSpeed, rb.velocity.y);  // 이동 값

        if (JumpA)
        {

            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
            nowMode = JumpAnime;

            //rb.velocity = new Vector2(rb.velocity.x, JumpPower);
            //nowMode = JumpAnime;
            JumpA = false;


        }


        this.GetComponent<Animator>().Play(nowMode);
        this.GetComponent<SpriteRenderer>().flipX = Playerfilp;
    }
    /* void Jump()
     {
         JumpA = true;
         JumpCounter++;
     }*/
}
