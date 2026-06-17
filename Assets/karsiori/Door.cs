using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public SceneChange sceneChanger; // Inspector에서 연결할 수 있도록 public 선언

    private bool isOpened = false;

    public float openMoveY = 2f;
    public float openSpeed = 2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpened) return;

        if (other.CompareTag("Player"))
        {
            if (PlayerInventory.HasKey)
            {
                isOpened = true;
                Debug.Log("문이 열렸습니다!");
                StartCoroutine(OpenDoor());
            }
            else
            {
                Debug.Log("열쇠가 필요합니다!");
            }
        }
    }

    IEnumerator OpenDoor()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(0, openMoveY, 0);

        while (Vector3.Distance(transform.position, endPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                endPos,
                openSpeed * Time.deltaTime
            );

            yield return null;

            // 이동 완료 후 호출
            SceneManager.LoadScene("StageSelect");
        }

        gameObject.SetActive(false);

    }
}