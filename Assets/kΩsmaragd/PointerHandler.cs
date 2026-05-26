using UnityEngine;
using UnityEngine.EventSystems;

public class PointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject StartArrow;
    public GameObject EndArrow;
    public bool isStartButton; // 인스펙터에서 START 버튼일 때만 체크


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isStartButton)
        {
            // START 버튼 위에 있을 때
            StartArrow.SetActive(true);
            EndArrow.SetActive(false);
        }
        else
        {
            // END 버튼 위에 있을 때 (isStartButton이 false인 경우)
            StartArrow.SetActive(false);
            EndArrow.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 마우스가 버튼 밖으로 나가면 무조건 둘 다 끄기
        StartArrow.SetActive(false);
        EndArrow.SetActive(false);
    }
}