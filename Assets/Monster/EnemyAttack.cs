using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private LayerMask PlayerLayer;

    [SerializeField]
    private float attackCooldown = 1.5f;

    [SerializeField]
    private int attackDamage = 10;

    private Animator anim;
    private PlayerController1 targetPlayer;

    private bool isPlayerInRange = false;
    private bool canAttack = true;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        RegisterPlayer(other);
        TryStartAttack();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        RegisterPlayer(other);
        TryStartAttack();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((PlayerLayer.value & (1 << other.gameObject.layer)) == 0) return;

        PlayerController1 player = other.GetComponent<PlayerController1>();
        if (player == null) return;

        if (player == targetPlayer)
        {
            isPlayerInRange = false;
            targetPlayer = null;
        }
    }

    void RegisterPlayer(Collider2D other)
    {
        if ((PlayerLayer.value & (1 << other.gameObject.layer)) == 0) return;

        PlayerController1 player = other.GetComponent<PlayerController1>();
        if (player == null) return;

        targetPlayer = player;
        isPlayerInRange = true;
    }

    void TryStartAttack()
    {
        if (!canAttack) return;
        if (!isPlayerInRange || targetPlayer == null) return;

        canAttack = false;

        if (anim != null)
        {
            anim.SetTrigger("Attack");
        }

        StartCoroutine(AttackCooldownRoutine());
    }

    // Animation Event에서 호출할 함수
    public void AttackHit()
    {
        if (!isPlayerInRange || targetPlayer == null) return;

        targetPlayer.Damage(attackDamage);
        Debug.Log("데미지 확인");
    }

    IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}