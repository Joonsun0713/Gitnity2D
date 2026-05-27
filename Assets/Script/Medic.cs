using UnityEngine;
using TMPro;

// Medic 클래스
// MonoBehaviour를 상속받아 Unity 오브젝트처럼 동작하게 함
public class Medic : MonoBehaviour
{
    // Inspector에서 조절 가능한 변수


    // 회복량
    // 예: 100이면 체력 100 회복
    public int healAmount = 100;

    // 상호작용 UI 연결용 변수
    // 여기에는 [E] 회복하기 텍스트를 넣을 예정
    public GameObject interactionUI;


    // 내부에서 사용할 변수

    // 플레이어가 여신상 근처에 있는지 저장
    // true  = 가까이 있음
    // false = 멀리 있음
    private bool playerNear = false;

    // 플레이어 체력 스크립트를 저장할 변수
    // 나중에 Heal() 함수를 사용하기 위해 필요
    private PlayerHealth playerHealth;


    // 게임 시작 시 1번 실행
    void Start()
    {
        // 게임 시작하자마자
        // 회복 UI는 안 보이게 설정
        interactionUI.SetActive(false);
    }


    // 매 프레임마다 계속 실행
    void Update()
    {
        // 플레이어가 가까이에 있고
        // E 키를 눌렀을 때
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            // 플레이어 체력 회복
            playerHealth.Heal(healAmount);

            // 콘솔에 로그 출력
            Debug.Log("체력을 회복했습니다!");
        }
    }



    // 플레이어가 Trigger 안으로 들어왔을 때
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트의 태그가 Player라면
        if (other.CompareTag("Player"))
        {
            // 플레이어가 가까이 있다고 표시
            playerNear = true;

            // 플레이어 오브젝트 안에 있는
            // PlayerHealth 스크립트를 가져옴
            playerHealth = other.GetComponent<PlayerHealth>();

            // [E] 회복하기 UI 보이기
            interactionUI.SetActive(true);
        }
    }


    // 플레이어가 Trigger 밖으로 나갔을 때
    private void OnTriggerExit2D(Collider2D other)
    {
        // 나간 오브젝트가 Player라면
        if (other.CompareTag("Player"))
        {
            // 플레이어 멀어짐
            playerNear = false;

            // UI 숨기기
            interactionUI.SetActive(false);
        }
    }
}