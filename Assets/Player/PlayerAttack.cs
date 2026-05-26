using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    float AtcurTime = 2.0f;
    public float AttackCoolTime = 1.5f;
    public Transform PlayerPos;
    public Vector2 bSize;

    int ComboStep = 0;  // �޺� ���� ���� �ܰ�
    float ComboTime = 0.0f; //�޺� �ð� ���
    float ComboDelay = 0.8f;
    bool isComboTimerRunning = false;

    float Combo_st = PlayerController1.Stamina;



    PlayerController1 PlayerControl;

    [SerializeField] 
    private LayerMask enemyLayer;

    void Start()
    {
        //ani = GetComponent<PlayerAnimator>();
        PlayerControl = GetComponentInParent<PlayerController1>();
    }

    // Update is called once per frame
    void Update()
    {
        AtcurTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0)) //���콺 ��Ŭ���� ���� ��
        {
            //ComboAttack();
          if (AtcurTime > AttackCoolTime)
             {
                ComboAttack();
                Debug.Log("����");
            }
            else
            {
                Debug.Log("���� ��Ÿ���� ���������ϴ�." + AtcurTime);
            }

        }

        if (isComboTimerRunning)
        {
            ComboTime += Time.deltaTime;
            if (ComboTime > ComboDelay) 
            {
                ComboStep = 0;
                ComboTime = 0f;
                isComboTimerRunning = false;
                PlayerControl.OnPlayerComboAttack(ComboStep);
                Debug.Log("�޺� �ʱ�ȭ" + ComboStep);
                AtcurTime = 0.0f;
            }
        }


    }
    //void ComboAttack()
    //{
    //    ComboTime = 0.0f;
    //    isComboTimerRunning =true;

    //    ComboStep++;
    //    if (ComboStep > 3)
    //        ComboStep = 1;
    //    Debug.Log("�޺� �ܰ�"+ ComboStep);
    //    PlayerControl.OnPlayerComboAttack(ComboStep);
    //    Attack();
    //    //Debug.Log(" ���� ����" );
    //    //AtcurTime = 0.0f;
    //}

    void ComboAttack()
    {
        // 스태미너 체크 (예: 1회당 10 소모)
        if (PlayerController1.Stamina >= 10)
        {
             PlayerControl.UseStamina(10); // 스태미너 소모

            ComboTime = 0.0f;
            isComboTimerRunning = true;
            ComboStep++;
            if (ComboStep > 3) ComboStep = 1;
            PlayerControl.OnPlayerComboAttack(ComboStep);
            Attack();
        }
        else
        {
            Debug.Log("스태미너가 부족합니다!");
        }
    }

    void Attack()
    {
        Collider2D[] collider2Ds =
        Physics2D.OverlapBoxAll(
            PlayerPos.position,
            bSize,
            0,
            enemyLayer
        );

        foreach (Collider2D collider in collider2Ds)
        {
        Debug.Log("공격 성공");
        EnemyHealth mh =
            collider.GetComponent<EnemyHealth>();
        if (mh != null)
        {
            mh.TakeDamage(1);
        }
    }
}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(PlayerPos.position, bSize);
    }

}
