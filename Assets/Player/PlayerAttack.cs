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
    float ComboTime = 0.0f; //콤보 시간 측정
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
       
        if (Input.GetMouseButtonDown(0)) //마우스 좌클릭시 공격 실행
        {
            
                ComboAttack();
                Debug.Log("����");
           

        }

        if (isComboTimerRunning)    // 실행 시 콤보간 시간 측정 시작
        {
            ComboTime += Time.deltaTime;    //콤보 시간 측정
            if (ComboTime > ComboDelay) //콤보 시간이 딜레이 시간을 넘어가면 콤보 초기화 
            {
                ComboStep = 0;  //콤보단계 초기화
                ComboTime = 0f; //콤보 시간 초기화
                isComboTimerRunning = false;    //콤보간 시간 측정 끄기
                PlayerControl.OnPlayerComboAttack(ComboStep); // 공격 애니메이션 끄기 
                Debug.Log("공격" + ComboStep);
               
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

            ComboTime = 0.0f;   // 콤보 시간 
            isComboTimerRunning = true; // 콤보간 시간 측정 켜기
            ComboStep++;    // 콤보 단계 올리기
            if (ComboStep > 3) ComboStep = 1;   // 콤보 단계가 3단계 이상 넘어갈 시 1단계로 되돌리기
            PlayerControl.OnPlayerComboAttack(ComboStep);   //ComboStep에 따른 공격 애니메이션 실행
            Attack();   // 공격 실행 
        }
        else
        {
            Debug.Log("스태미너가 부족합니다!");
        }
    }

    void Attack()   // 공격 메서드 
    {
        Collider2D[] collider2Ds =  Physics2D.OverlapBoxAll(PlayerPos.position,bSize,0,enemyLayer ); // 특정 범위 안에 있는 레이어를 감지
                                                                                                     // (위치, 박스 크기, 회전각도, 특정 레이어)
                                                                                                     //Collider2D[] 배열에 collider2Ds의 정보 담기

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
