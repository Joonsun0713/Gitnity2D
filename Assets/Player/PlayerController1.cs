using System;
using System.Collections;
using System.Collections.Generic;

using System.Security.Cryptography;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{

    Rigidbody2D rb;
    float Hz;
    [SerializeField]
    float JumpPower = 5.0f; // ������ 
    bool JumpA = false;  //���� �Ǵ�
    [SerializeField]
    float MoveSpeed = 5.0f; //�̵� �ӵ�
    [SerializeField]
    int JumpCounter = 0; // ���� Ƚ�� ����
    SpriteRenderer sprite;
    bool onGround = false;  //���� ����
    bool Playerfilp = false;  // �÷��̾� �¿����

    public Transform PlayerPos;
    public Vector2 bSize;

    float AtcurTime = 0.0f;
    public float AttackCoolTime = 1.5f;
    float AttackStop= 1.0f;
    Animator animator;
    // �ִϸ��̼� ���� 
    /* public string StandardAnime = "";
     public string MovingAnime = "";
     public string JumpAnime = "";
     public string DownAnime = "";
     public string RollAnime = "";*/

    //string nowMode = "";    //���� �ִϸ��̼� ����


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // �÷��̾� ������Ʈ�� ȸ������ �ʰ� �ϱ� 
        //GetComponent<Animator>().Play(nowMode);
        //GetComponent<SpriteRenderer>().flipX = Playerfilp;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        AtcurTime += Time.deltaTime;
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), 1 << 6);
        if (onGround) // �����ϸ� ���� Ƚ�� �ʱ�ȭ
        {
            JumpCounter = 0;
        }
        Hz = Input.GetAxisRaw("Horizontal"); //�̵�Ű �� �ޱ�

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
        else
        {
            animator.SetBool("Move", false);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround || JumpCounter < 1) // ������ �ְų�, ���� Ƚ���� ����ϸ� ���� ����
            {
                UnityEngine.Debug.Log("������ JumpCounter:" + JumpCounter);
                JumpA = true;
                ++JumpCounter;

            }

        }
        /*if (onGround) // �����ϸ� ���� Ƚ�� �ʱ�ȭ
        {
            JumpCounter = 0;
        }*/
        // UnityEngine.Debug.Log("������ JumpCounter:"+JumpCounter);
        //onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.2f), 1 << 6);

        // �÷��̾� ����

        if (Input.GetMouseButtonDown(0)) //���콺 ��Ŭ���� ���� ��
        {
            //����

            if (AtcurTime > AttackCoolTime)
            {
                animator.SetTrigger("Attack");
                Attack();
                AtcurTime = 0.0f;
                //AttackStop = 0.0f;

            }
            else
            {
                Debug.Log("���� ��Ÿ���� ���������ϴ�."+ AtcurTime);
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
                   EnemyHealth mh = collider.GetComponent<EnemyHealth>();
                    if (mh != null)
                    {
                    mh.TakeDamage(1);
                    }
                     Debug.Log("피격");
            }
        }
        AttackStop = 1.0f;
    }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(PlayerPos.position, bSize);
        }
        void FixedUpdate()
        {
            // onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.2f), 1 << 6);
            rb.velocity = new Vector2(Hz * MoveSpeed*AttackStop, rb.velocity.y*AttackStop);  // �̵� ��

            if (JumpA)
            {

                rb.velocity = new Vector2(rb.velocity.x, JumpPower);
                //nowMode = JumpAnime;

                //rb.velocity = new Vector2(rb.velocity.x, JumpPower);
                //nowMode = JumpAnime;
                JumpA = false;


            }

            sprite.flipX = Playerfilp;

        }
        /* void Jump()
         {
             JumpA = true;
             JumpCounter++;
         }*/
   
}
