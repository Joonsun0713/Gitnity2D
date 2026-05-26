using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Image hpBar;
    // PlayerController1의 PlayerLife 초기값과 동일하게 10f로 설정합니다.
    public float maxHp = 10f;

    void Update()
    {
        // PlayerController1의 static 변수인 PlayerLife를 가져옵니다.
        float currentHp = (float)PlayerController1.PlayerLife;

        // 체력이 0보다 작아지지 않게 방지하고, 비율을 계산합니다.
        hpBar.fillAmount = Mathf.Clamp01(currentHp / maxHp);
    }
}