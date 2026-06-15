using UnityEngine;
using Cainos.PixelArtPlatformer_VillageProps;

public class TreasureChestTrigger : MonoBehaviour
{
    public GameObject keyPrefab;
    public Transform keySpawnPoint;

    private bool isOpened = false;
    private Chest chest;

    void Start()
    {
        chest = GetComponent<Chest>();

        if (chest == null)
        {
            Debug.LogError("상자에 Chest 스크립트가 없습니다!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("상자 충돌 감지: " + other.gameObject.name);

        if (isOpened) return;

        if (other.CompareTag("Player"))
        {
            isOpened = true;

            if (chest != null)
            {
                chest.Open();
            }

            Vector3 spawnPosition = transform.position + new Vector3(0, 1f, 0);

            if (keySpawnPoint != null)
            {
                spawnPosition = keySpawnPoint.position;
            }

            if (keyPrefab != null)
            {
                Instantiate(keyPrefab, spawnPosition, Quaternion.identity);
            }

            Debug.Log("상자 열림! 열쇠 생성!");
        }
    }
}