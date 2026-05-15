using System;
using System.Collections;
using System.Collections.Generic;

//using System.Diagnostics;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    //public bool JumpA = false;
    bool onGround = false;
    int JumpCounter = 0;

    PlayerController1 PJumpA;
    void Start()
    {
        PJumpA = GetComponentInParent<PlayerController1>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.right * 0.1f), 1 << 6);
        if (onGround) // 착지하면 점프 횟수 초기화
        {
            Debug.Log("착지중");
            JumpCounter = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround || JumpCounter < 2) // 착지해 있거나, 점프 횟수가 충분하면 점프 가능
            {
                UnityEngine.Debug.Log("현재의 JumpCounter:" + JumpCounter);
                PJumpA.JumpA = true;
                ++JumpCounter;

            }

        }
       
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // 현재 코드의 Linecast와 완전히 동일한 위치에 선을 그립니다.
        Gizmos.DrawLine(transform.position, transform.position - (transform.right * 0.1f));
    }
}
