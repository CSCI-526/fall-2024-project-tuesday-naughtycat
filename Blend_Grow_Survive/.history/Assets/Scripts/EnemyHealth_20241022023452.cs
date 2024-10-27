using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;


public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public TextMeshProUGUI healthText;
    public bool hasBeenShot = false;


    void Start()
    {
        currentHealth = Mathf.RoundToInt(transform.localScale.x);

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

