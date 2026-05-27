using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 5;          // 몬스터의 최대 체력
    private int currentHp;
    private bool isDead = false;    // 몬스터 데스 확인

    [SerializeField]
    private string EnemyName = "Monster";

    void Start()
    {
        currentHp = maxHp;

        if (EnemyName == "Goblin")
        {
            currentHp = 10;
        }
        else if (EnemyName == "Skeleton")
        {
            currentHp = 5;
        }
        else if (EnemyName == "Mushroom")
        {
            currentHp = 20;
        }
    
    
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHp -= damage;

        // --- UI 갱신 로직 추가 ---
        // UI 매니저의 싱글톤 인스턴스에 접근하여 현재 정보 전달
        float hpPercent = (float)currentHp / maxHp;
        TargetUI.Instance.SetTarget(EnemyName, hpPercent);
        // -------------------------


        Debug.Log(gameObject.name + " 피격");

        if (currentHp <= 0)
        {
            Die();
        }

        // EnemyHealth.cs의 TakeDamage 끝부분에 추가
        // 공격받을 때마다 타이머 초기화 후 2초 뒤 사라지게 하기
        TargetUI.Instance.CancelInvoke("HideUI");
        TargetUI.Instance.Invoke("HideUI", 2.0f);
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