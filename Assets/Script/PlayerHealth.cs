using UnityEngine;

// PlayerHealth 클래스
// 플레이어의 체력을 관리하는 스크립트
// MonoBehaviour를 상속받아 Unity 오브젝트처럼 동작함
public class PlayerHealth : MonoBehaviour
{
    // =========================
    // 체력 변수
    // =========================

    // 플레이어의 최대 체력
    // Inspector에서 수정 가능
    // 예: 100
    public int maxHealth = 100;

    // 현재 체력
    // 실제 게임 중 계속 변하는 값
    public int currentHealth;


    // =========================
    // 게임 시작 시 1번 실행
    // =========================
    void Start()
    {
        // 게임 시작할 때
        // 현재 체력을 최대 체력으로 설정
        currentHealth = maxHealth;

        // 콘솔에 로그 출력
        Debug.Log("플레이어 체력 초기화 완료");
    }


    // =========================
    // 체력 회복 함수
    // =========================
    // 다른 스크립트에서 호출 가능
    // 예:
    // playerHealth.Heal(20);
    //
    // amount = 회복할 체력량
    public void Heal(int amount)
    {
        // 현재 체력 증가
        currentHealth += amount;

        // 현재 체력이 최대 체력을 넘으면 안 되니까 제한
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // 현재 체력 상태 출력
        Debug.Log("회복됨! 현재 체력 : " + currentHealth);
    }


    // =========================
    // 데미지 받는 함수
    // =========================
    // 몬스터 공격이나 함정 등에 사용 가능
    //
    // damage = 받을 데미지 양
    public void TakeDamage(int damage)
    {
        // 현재 체력 감소
        currentHealth -= damage;

        // 데미지 로그 출력
        Debug.Log("피격! 현재 체력 : " + currentHealth);

        // 체력이 0 이하인지 검사
        if (currentHealth <= 0)
        {
            // 사망 처리 함수 실행
            Die();
        }
    }


    // =========================
    // 플레이어 사망 함수
    // =========================
    void Die()
    {
        // 콘솔 출력
        Debug.Log("플레이어 사망");

        // 나중에 여기에 추가 가능:
        //
        // 1. 죽는 애니메이션
        // 2. 게임 오버 UI
        // 3. 리스폰 시스템
        // 4. 체크포인트 이동
        // 5. 사운드 재생
    }
}