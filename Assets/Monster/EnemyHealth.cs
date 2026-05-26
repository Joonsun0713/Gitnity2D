using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 5;

    private int currentHp;

    private bool isDead = false;

    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHp -= damage;

        Debug.Log(gameObject.name + " 피격");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        Debug.Log(gameObject.name + " 사망");

        // 이동 정지
        EnemyMove move = GetComponent<EnemyMove>();

        if (move != null)
        {
            move.enabled = false;
        }

        // 충돌 제거
        Collider2D col = GetComponent<Collider2D>();

        if (col != null)
        {
            col.enabled = false;
        }

        Destroy(gameObject, 1.0f);
    }
}