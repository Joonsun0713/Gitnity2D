using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 5;          // 몬스터의 최대 체력
    private int currentHp;          // 몬스터의 현재 체력 설정

    [SerializeField]
    private string EnemyName;       // 몬스터의 이름 Inspector을 통하여, 이름 수정 가능
    private Animator animator;



    void Start()
    {
        switch (EnemyName)          // Inspector 에 있는 몬스터의 이름에 따라 최대 체력이 달라진다.
        {
            case "Goblin": maxHp = 3; break;
            case "Skeleton": maxHp = 5; break;
            case "Mushroom": maxHp = 10; break;
        }
        animator = GetComponent<Animator>();
        currentHp = maxHp; // 공통으로 한 번만 할당
    }

    public void TakeDamage(int damage)  // 몬스터 체력 계산 및 UI 갱신
    {
        if (TargetUI.Instance == null)
        {
            TargetUI.Instance = FindObjectOfType<TargetUI>();
        }

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
                // Invoke 를 통하여 2초 후에 HIdeUI 가 작동하도록 설정
                // Invoke 취소, 연속 공격 기능도 있기에 공격하다가 Invoke 가 동작하는 것을 방지. 
            }
        }

        Debug.Log(gameObject.name + "에게 데미지 전달됨");
    }

    void Die()
    {
        Debug.Log(gameObject.name + "가 Die() 함수에 진입했습니다.");

        // 1. UI 처리
        if (TargetUI.Instance != null && TargetUI.Instance.uiPanel != null) // uiPanel까지 체크
        {
            TargetUI.Instance.CancelInvoke("HideUI");
            TargetUI.Instance.HideUI();
        }

        // 2. 다른 애니메이션 진행 취소 (핵심 부분)
        // 공격이나 이동 관련 트리거가 남아있다면 모두 초기화합니다.
        animator.ResetTrigger("Attack");
        animator.SetInteger("WalkSpeed", 0);
        // 만약 다른 트리거(예: Run, Idle 등)를 사용 중이라면 동일하게 ResetTrigger를 추가하세요.

        // 3. 이동 정지 및 충돌 제거
        EnemyMove move = GetComponent<EnemyMove>();
        if (move != null) move.enabled = false;

        // 4. Die 애니메이션 재생
        // 다른 상태에 머물러 있지 않도록 강제로 Play를 사용하는 방법도 있습니다.
        // animator.Play("GoblinDie_Animation"); // 특정 레이어의 특정 애니메이션을 즉시 재생
        animator.SetTrigger("isDie");

        // 5. 1초 뒤 오브젝트 삭제
        Destroy(gameObject, 1.5f);
    }

}