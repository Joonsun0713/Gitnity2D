using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    EnemyAttack attackScript; // 선언

    
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
        attackScript = GetComponent<EnemyAttack>();
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
        // 공격 중이면 이동 애니메이션 업데이트를 건너뜀 (안전하게 체크)
        if (attackScript != null && attackScript.isAttacking)
        {
            // 공격 중일 때는 이동 속도를 0으로 만들어 멈추게 함
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            return;
        }

        // 1. 추격 로직 (기존과 동일)
        if (isChasing && player)
        {
            if (player.position.x > transform.position.x)
            {
                nextMove = 1;
                spriteRenderer.flipX = false;
            }
            else if (player.position.x < transform.position.x) // 수정: else 대신 조건 추가
            {
                nextMove = -1;
                spriteRenderer.flipX = true;
            }

            // 절벽 체크 로직...
        }

        // ... 기존 이동 및 애니메이션 코드 ...
        bool isMoving = Mathf.Abs(nextMove) > 0;
        anim.SetBool("Run", isMoving);
        anim.SetBool("Idle", !isMoving);

        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
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