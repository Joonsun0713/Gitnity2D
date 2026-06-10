using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public int nextMove;

    private Transform player;
    private float detectRange = 5f;
    private float loseRange = 7f;

    private bool isChasing = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // ... 기존 플레이어 찾기 로직 동일 ...
        StartCoroutine(PatrolRoutine());
    }

    IEnumerator PatrolRoutine()
    {
        while (!isChasing) // 쫓고 있지 않을 때만 실행
        {
            // 1. 랜덤 방향 결정 (-1, 0, 1)
            nextMove = Random.Range(-1, 2);

            // 2. 방향에 따른 애니메이션/플립 처리
            if (nextMove != 0)
            {
                spriteRenderer.flipX = (nextMove == -1);
            }

            // 3. 정해진 시간 동안 이동
            float moveTime = Random.Range(2f, 4f);
            yield return new WaitForSeconds(moveTime);

            // 4. 잠시 멈춤 (대기)
            nextMove = 0;
            yield return new WaitForSeconds(1f);
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (!isChasing && distance <= detectRange)
        {
            isChasing = true;
            CancelInvoke("Think");
        }
        else if (isChasing && distance >= loseRange)
        {
            isChasing = false;
            if (!IsInvoking("Think"))
            {
                Invoke("Think", 1f);
            }
        }
    }

    void FixedUpdate()
    {
        if (isChasing && player)
        {
            if (player.position.x > transform.position.x)
            {
                nextMove = 1;
                spriteRenderer.flipX = false;
            }
            else
            {
                // 배회 중일 때만 레이캐스트 검사
                Vector2 frontVec = new Vector2(rigid.position.x + (nextMove * 0.5f), rigid.position.y - 0.5f);
                RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 1f, LayerMask.GetMask("ground"));

                if (rayHit.collider == null && nextMove != 0)
                {
                    // 절벽 발견 시 이동 정지 후 코루틴 내에서 다음 로직 수행
                    nextMove = 0;
                }
            }
        }

        //rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.5f, rigid.position.y - 0.5f);
        //Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        //RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 1f, LayerMask.GetMask("ground"));

        //if (rayHit.collider == null)
        //{
        //    Turn();
        //}

        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        anim.SetInteger("WalkSpeed", Mathf.Abs(nextMove));
    }

    void Think()
    {
        if (isChasing) return;

        nextMove = Random.Range(-1, 2);

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 2f);
    }
}