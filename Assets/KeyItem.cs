using UnityEngine;

public class KeyItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory.HasKey = true;

            Debug.Log("열쇠 획득!");

            Destroy(gameObject);
        }
    }
}
