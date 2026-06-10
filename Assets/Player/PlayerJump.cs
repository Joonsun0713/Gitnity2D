using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerJump : MonoBehaviour
{
   
    public bool onGround = false;
    int JumpCounter = 0;

    // Update is called once per frame
    void Update()
    {
        if (PlayerController1.IsDead) return;
        onGround = Physics2D.Linecast(transform.position, (Vector2)transform.position - (Vector2.up * 0.1f), 1 << 6);
        if (onGround) // 착지하면 점프 횟수 초기화
        {
            //Debug.Log("착지중");
            JumpCounter = 0;
            Debug.Log("점프 카운터 " + JumpCounter +"OnGround "+onGround);
        }
        
       
        if (Input.GetKeyDown(KeyCode.Space)&& !PlayerController1.isShield)
        {
            if (onGround ||JumpCounter < 1) // 착지해 있거나, 점프 횟수가 충분하면 점프 가능
            {
                //UnityEngine.Debug.Log("현재의 JumpCounter:" + JumpCounter);
                PlayerController1.JumpA = true;
                //PJump.OnPlayerJumpUp();
                Debug.Log("점프 카운터 올리기 전 " + JumpCounter);
                JumpCounter++;
                Debug.Log("카운터 올린 후 " + JumpCounter);
            }

        }
        //Debug.Log("onGround = "+ onGround);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.position - (transform.up * 0.2f));
        Gizmos.DrawLine(transform.position, transform.position - (transform.up * 0.1f));
    }
}
