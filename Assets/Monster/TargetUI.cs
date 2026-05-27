using UnityEngine;
using UnityEngine.UI;

public class TargetUI : MonoBehaviour
{
    public static TargetUI Instance; // 어디서든 접근 가능하게 싱글톤 패턴 사용

    public Text nameText;    // 몬스터 이름 표시용 Text
    public Image healthBar;  // 몬스터 체력 바 (Fill Amount 방식)
    public GameObject uiPanel; // 중앙 UI 패널

    void Awake() => Instance = this;

    // 타겟이 바뀔 때 호출
    public void SetTarget(string name, float healthPercent)
    {
        uiPanel.SetActive(true); // UI 켜기
        nameText.text = name;
        healthBar.fillAmount = healthPercent;
    }

    // TargetUI.cs에 추가할 내용
    public void HideUI()
    {
        uiPanel.SetActive(false);
    }


}