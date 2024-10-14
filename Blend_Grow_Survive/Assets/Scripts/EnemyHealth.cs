using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro; 
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public TextMeshProUGUI healthText; 
    

    void Start()
    {
        currentHealth = maxHealth;
        
        healthText = GetComponentInChildren<TextMeshProUGUI>(true); 
        UpdateHealthText();
    }

   
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    
    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }

    
    void Die()
    {
        
        Destroy(gameObject);  
    }
}

