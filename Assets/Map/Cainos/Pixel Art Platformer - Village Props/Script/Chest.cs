using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject keyPrefab;

    private bool opened = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !opened)
        {
            opened = true;

            // 상자 열림 애니메이션 실행
            if (animator != null)
            {
                animator.SetBool("IsOpened", true);
            }

            // 열쇠 생성
            Instantiate(
                keyPrefab,
                transform.position + new Vector3(0, 1.5f, 0),
                Quaternion.identity
            );

            Debug.Log("상자 열림!");
        }
    }
}