using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static bool HasKey = false;

    void Start()
    {
        HasKey = false;
    }

    public void GetKey()
    {
        HasKey = true;
        Debug.Log("열쇠 획득!");
    }
}