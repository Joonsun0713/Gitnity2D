using UnityEngine;

// KeyItem 클래스
// 열쇠 아이템을 관리하는 스크립트
// 플레이어가 열쇠에 닿으면 획득하고 사라지게 함
public class KeyItem : MonoBehaviour
{
    // 플레이어가 Trigger 영역 안으로 들어왔을 때 실행
    // Collider2D의 Is Trigger가 체크되어 있어야 동작함
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트의 Tag가 "Player"인지 확인
        // 즉 플레이어가 닿았는지 검사
        if (other.CompareTag("Player"))
        {
            // 콘솔창에 메시지 출력
            Debug.Log("열쇠를 획득했습니다!");

            // 현재 오브젝트(열쇠)를 삭제
            // 게임 화면에서 사라짐
            Destroy(gameObject);
        }
    }
}