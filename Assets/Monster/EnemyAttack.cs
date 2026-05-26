using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private LayerMask PlayerLayer;

    private bool canAttack = true;

    [SerializeField]
    private float attackCooldown = 1.5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!canAttack) return;

        if ((PlayerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            PlayerController1 targetPlayer =
                other.GetComponent<PlayerController1>();

            if (targetPlayer != null)
            {
                targetPlayer.Damage(10);

                Debug.Log("데미지 확인");

                StartCoroutine(AttackCooldown());
            }
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}