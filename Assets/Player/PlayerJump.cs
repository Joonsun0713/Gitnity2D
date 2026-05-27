using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    //public bool JumpA = false;
    public bool onGround = false;
    int JumpCounter = 0;

    PlayerController1 PJump;
    void Start()
    {
        PJump = GetComponentInParent<PlayerController1>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.Linecast(transform.position, (Vector2)transform.position - (Vector2.up * 0.2f), 1 << 6);
        if (onGround) // 착지하면 점프 횟수 초기화
        {
            
            //Debug.Log("착지중");
            JumpCounter = 0;

        }
        
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround ||JumpCounter < 1) // 착지해 있거나, 점프 횟수가 충분하면 점프 가능
            {
                //UnityEngine.Debug.Log("현재의 JumpCounter:" + JumpCounter);
                PJump.JumpA = true;
                //PJump.OnPlayerJumpUp();
                JumpCounter++;

            }

        }
        //Debug.Log("onGround = "+ onGround);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.position - (transform.up * 0.2f));
        Gizmos.DrawLine(transform.position, transform.position - (transform.up * 0.2f));
    }
}
