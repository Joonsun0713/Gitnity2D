using UnityEngine;
using UnityEngine.UI; // UI Image를 쓰기 위해 필요

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;

    // HP 바 이미지 연결용
    public Image hpBar;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHpUI();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHpUI();

        Debug.Log("회복됨! 현재 체력 : " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        UpdateHpUI();

        Debug.Log("피격! 현재 체력 : " + currentHealth);
    }

    void UpdateHpUI()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}