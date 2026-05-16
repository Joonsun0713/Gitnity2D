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
            
            if (AtcurTime > AttackCoolTime)
            {
                PlayerControl.OnPlayerAttack();
                Attack();
                AtcurTime = 0.0f;
                Debug.Log("공격");
            }
            else
            {
                Debug.Log("아직 쿨타임이 안지났습니다." + AtcurTime);
            }

        }

    }

    void Attack()
    {


        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(PlayerPos.position, bSize, 0, enemyLayer);

        foreach (Collider2D collider in collider2Ds)
        {
           
                Debug.Log("공격 성공");

        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(PlayerPos.position, bSize);
    }

}
