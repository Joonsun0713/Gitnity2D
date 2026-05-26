using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHp = 5;
    int currentHp;
    Animator anim;
    bool isDead = false;
    void Start()
    {
        currentHp = maxHp;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHp -= damage;
        Debug.Log(gameObject.name + " 피격!");
        // 피격 애니메이션
        if (anim != null)
        {
            anim.SetTrigger("Hit");
        }
        // 죽음 체크
        if (currentHp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        isDead = true;
        Debug.Log(gameObject.name + " 사망!");
        // 죽는 애니메이션
        if (anim != null)
        {
            anim.SetTrigger("Death");
        }
        // 움직임 멈춤
        GetComponent<EnemyMove>().enabled = false;
        // 콜라이더 제거
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }
        // 바로 삭제 말고 약간 기다림
        Destroy(gameObject, 1.0f);
    }
}