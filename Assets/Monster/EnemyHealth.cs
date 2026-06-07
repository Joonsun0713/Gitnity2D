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

        if (EnemyName == "Goblin")
        {
            maxHp = 5;
            currentHp = maxHp;
        }
        else if (EnemyName == "Skeleton")
        {
            maxHp = 10;
            currentHp = maxHp;
        }
        else if (EnemyName == "Mushroom")
        {
            maxHp = 20;
            currentHp = maxHp;
        }
        


    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHp -= damage;

        Debug.Log(EnemyName + " 현재 체력: " + currentHp);

        if (currentHp <= 0)
        {
            Die(); // 반드시 여기서 Die()가 호출되어야 합니다.
        }
        else
        {
            // 죽지 않았을 때만 UI 갱신 및 다시 숨기기 예약
            if (TargetUI.Instance != null)
            {
                float hpPercent = (float)currentHp / maxHp;
                TargetUI.Instance.SetTarget(EnemyName, hpPercent);

                TargetUI.Instance.CancelInvoke("HideUI");
                TargetUI.Instance.Invoke("HideUI", 2.0f);
            }
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + "가 Die() 함수에 진입했습니다.");
        isDead = true;

        // 1. 죽는 즉시 UI를 숨깁니다.
        if (TargetUI.Instance != null)
        {
            TargetUI.Instance.CancelInvoke("HideUI"); // 기존에 예약된 숨기기 취소
            TargetUI.Instance.HideUI();              // 즉시 UI 숨기기
        }

        Debug.Log(gameObject.name + " 사망");

        // 2. 이동 정지 및 충돌 제거
        EnemyMove move = GetComponent<EnemyMove>();
        if (move != null) move.enabled = false;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // 3. 1초 뒤 오브젝트 삭제
        Destroy(gameObject, 1.0f);
    }
}