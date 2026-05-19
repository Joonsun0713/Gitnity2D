using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    EnemyMove EnemyAnime;
    PlayerController1 Player;
    [SerializeField]
    private LayerMask PlayerLayer;
    public Transform EnemyPos;
    public Vector2 bSize;
    
    void Start()
    {
        EnemyAnime = GetComponentInParent<EnemyMove>();
        Player = GetComponent<PlayerController1>();
    }

    
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if ((PlayerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            
            PlayerController1 targetPlayer = other.GetComponent<PlayerController1>();

           
            if (targetPlayer != null)
            {
                // 공격 애니메이션 실행
                if (EnemyAnime != null) EnemyAnime.EnemyAttackAnimation();

                // 플레이어에게 데미지 1 주기!
                targetPlayer.Damage(1);
                Debug.Log("플레이어 공격 성공 및 데미지 전달 완료!");
            }
        }
    }
    /* void OnTriggerEnter2D(Collider2D other)
     {
         Attack();
         EnemyAnime.EnemyAttackAnimation();
     }

     void Attack()
     {


         Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(EnemyPos.position, bSize, 0, PlayerLayer);

         foreach (Collider2D collider in collider2Ds)
         {
             Player.Damage(1);
             Debug.Log("플레이어 공격 성공");

         }
     }*/
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(EnemyPos.position, bSize);
    }
}
