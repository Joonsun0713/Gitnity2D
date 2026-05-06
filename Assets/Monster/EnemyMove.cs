using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator anim;
    SpriteRenderer spriteRenderer;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    // 애니메이션
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Flip", 2);


    }
    

    void SetDirection(int dir)
    {
        nextMove = dir;

 
        // 애니메이션
        anim.SetInteger("WalkSpeed", Mathf.Abs(nextMove));
;

        // 방향
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == -1;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.5f, rigid.position.y - 0.5f);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        // RayCast를 사용하여 몬스터 주변에 바닥이 없을 시 방향 전환을 하여 안떨어지게 할 수 있음.
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 1f, LayerMask.GetMask("ground"));



        if (rayHit.collider == null) // 바닥이 없을 시 방향 전환
        {
            Turn();
        }
    }
    void Flip() // 움직임 구현
    {
        int rand = Random.Range(-1, 2);

        SetDirection(rand);

        float nextFlipTime = Random.Range(2f, 4f);  // 2~4초 랜덤 시간 부여
        Invoke("Flip", nextFlipTime);               // 2~4초마다 Flip 호출 하면서 움직이게 함. 재귀함수.
    }

    void Turn() // 방향전환
    {
        SetDirection(-nextMove);

        CancelInvoke();
        Invoke("Flip", 2);  // 2초마다 Flip 호출
    }
}

