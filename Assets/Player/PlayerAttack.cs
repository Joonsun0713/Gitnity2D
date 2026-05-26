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

    int ComboStep = 0;  // 콤보 공격 현재 단계
    float ComboTime = 0.0f; //콤보 시간 재기
    float ComboDelay = 0.8f;
    bool isComboTimerRunning = false;





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

        if (Input.GetMouseButtonDown(0)) //마우스 좌클릭을 했을 때
        {
            //ComboAttack();
          if (AtcurTime > AttackCoolTime)
             {
                ComboAttack();
                Debug.Log("공격");
            }
            else
            {
                Debug.Log("아직 쿨타임이 안지났습니다." + AtcurTime);
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
                Debug.Log("콤보 초기화" + ComboStep);
                AtcurTime = 0.0f;
            }
        }


    }
    void ComboAttack()
    {
        ComboTime = 0.0f;
        isComboTimerRunning =true;

        ComboStep++;
        if (ComboStep > 3)
            ComboStep = 1;
        Debug.Log("콤보 단계" + ComboStep);
        PlayerControl.OnPlayerComboAttack(ComboStep);
        Attack();
        //Debug.Log(" 공격 성공" );
        //AtcurTime = 0.0f;
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
