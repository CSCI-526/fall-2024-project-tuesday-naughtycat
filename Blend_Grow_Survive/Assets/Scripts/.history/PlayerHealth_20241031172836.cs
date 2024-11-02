// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class PlayerHealth : MonoBehaviour
// {
//     public int currentHealth;
//     public float sizeReductionAmount = 0.5f; // Amount to reduce player size per hit
//     public TextMeshProUGUI healthText; // Reference to display player's health
//     private PlayerEat playerEat; // Reference to PlayerEat for end game
//     private float minScale = 0.5f;

//     void Start()
//     {
//         currentHealth = Mathf.RoundToInt(transform.localScale.x);

//         healthText = GetComponentInChildren<TextMeshProUGUI>(true);

//         UpdateHealthText();
//     }

//     public void TakeDamage(int damage)
//     {
//         currentHealth -= damage;
//         UpdateHealthText();
//         ReducePlayerSize();
//         if (currentHealth <= 0)
//         {
//             Die();
//         }
//     }

//     void UpdateHealthText()
//     {
//         if (healthText != null)
//         {
//             healthText.text = currentHealth.ToString();
//         }
//     }

//     void ReducePlayerSize()
//     {
//         Vector3 newScale = transform.localScale - new Vector3(sizeReductionAmount, sizeReductionAmount, 0f);

//         // Ensure player size doesn’t go below the minimum scale
//         if (newScale.x <= minScale || newScale.y <= minScale)
//         {
//             Die(); // Trigger GameOver if size is too small
//         }
//         else
//         {
//             transform.localScale = newScale;
//         }
//     }
//     void Die()
//     {
//         if (playerEat != null)
//         {
//             playerEat.GameOver(); // End the game if player health reaches zero
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public float sizeReductionAmount = 0.5f; // Amount to reduce player size per hit
    public TextMeshProUGUI healthText; // Reference to display player's health
    public PlayerEat playerEat; // Reference to PlayerEat for end game
    private float minScale = 0.5f; // Minimum scale threshold for player death

    void Start()
    {
        currentHealth = Mathf.RoundToInt(transform.localScale.x);
        healthText = GetComponentInChildren<TextMeshProUGUI>(true);
        UpdateHealthText();
    }

    void Update()
    {
        // Continuously check if player's size is below the minimum threshold
        if (transform.localScale.x < minScale || transform.localScale.y < minScale)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthText();
        ReducePlayerSize();
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

    void ReducePlayerSize()
    {
        Vector3 newScale = transform.localScale - new Vector3(sizeReductionAmount, sizeReductionAmount, 0f);

        // Ensure player size doesn’t go below the minimum scale
        if (newScale.x < minScale || newScale.y < minScale)
        {
            Die(); // Trigger GameOver if size is too small
        }
        else
        {
            transform.localScale = newScale;
        }
    }

    void Die()
    {
        if (playerEat != null)
        {
            playerEat.GameOver(); // End the game if player health reaches zero or size is too small
        }
        else
        {
            Debug.LogError("PlayerEat reference is missing!");
        }
    }
}
