using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public TextMeshProUGUI healthText; // Reference to display player's health
    private PlayerEat playerEat; // Reference to PlayerEat for end game

    void Start()
    {
        currentHealth = maxHealth;
        playerEat = GetComponent<PlayerEat>();

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
            healthText.text = "Health: " + currentHealth.ToString();
        }
    }

    void Die()
    {
        if (playerEat != null)
        {
            playerEat.GameOver(); // End the game if player health reaches zero
        }
    }
}
