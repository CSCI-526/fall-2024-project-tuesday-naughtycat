using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro; // 如果使用 TextMeshPro
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;  // 最大血量
    private int currentHealth;  // 當前血量
    public TextMeshProUGUI healthText; // 顯示血量的TextMeshPro組件

    void Start()
    {
        currentHealth = maxHealth;
        // 自動尋找子物件中的 TextMeshPro 組件
        healthText = GetComponentInChildren<TextMeshProUGUI>(true); // true 表示包含非活躍物件
        UpdateHealthText();
    }

    // 減少敵人血量的方法
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
         Debug.Log("輸出當前血量到控制台"); // 輸出當前血量到控制台
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 更新頭頂的血量顯示
    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }

    // 死亡處理
    void Die()
    {
        // 播放死亡動畫或音效
        Destroy(gameObject);  // 摧毀敵人
    }
}

