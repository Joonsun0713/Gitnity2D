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
    Invoke("Think", 2f);
    GameObject p =
        GameObject.FindGameObjectWithTag("Player");

    if (p == null)
    {
        p = GameObject.FindGameObjectWithTag("Player");
    }

    if (p != null)
    {
        player = p.transform;

        Debug.Log("플레이어 찾음");
    }
    else
    {
        Debug.Log("플레이어 못찾음");
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
        if (isChasing && player != null)
        {
            if (player.position.x > transform.position.x)
            {
                nextMove = 1;
                spriteRenderer.flipX = false;
            }
            else
            {
                nextMove = -1;
                spriteRenderer.flipX = true;
            }
        }

        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.5f, rigid.position.y - 0.5f);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector2.down, 1f, LayerMask.GetMask("ground"));
        if (rayHit.collider == null)
        {
            Turn();
        }
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