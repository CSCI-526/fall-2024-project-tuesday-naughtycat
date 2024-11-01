using UnityEngine;

public class PlayerSizeMonitor : MonoBehaviour
{
    public float minScale = 0.5f; // Minimum scale before player dies
    private PlayerHealth playerHealth; // Reference to PlayerHealth script
    PlayerEat playerEat;

    void Start()
    {
        // Get the PlayerHealth component attached to the player
        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component not found on the player. Make sure PlayerHealth is attached.");
        }
    }

    void Update()
    {
        // Check if the player's scale falls below the minimum threshold
        if (transform.localScale.x < minScale || transform.localScale.y < minScale)
        {
            TriggerPlayerDeath();
        }
    }

    void TriggerPlayerDeath()
    {
        // If player size is too small, trigger the the game over in PlayerEat
        playerEat = GetComponent<PlayerEat>();
        if (playerEat != null)
        {
            playerEat.GameOver();
        }

    }
}
