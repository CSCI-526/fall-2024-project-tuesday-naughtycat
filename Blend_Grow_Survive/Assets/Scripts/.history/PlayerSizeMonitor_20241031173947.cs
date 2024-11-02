using UnityEngine;

public class PlayerSizeMonitor : MonoBehaviour
{
    public float minScale = 0.5f; // Minimum scale before player dies
    private PlayerHealth playerHealth; // Reference to PlayerHealth script

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
        if (playerHealth != null)
        {
            playerHealth.Die(); // Call the Die() method in PlayerHealth script to handle the player's death
        }
        else
        {
            Debug.LogError("PlayerHealth component is missing. Unable to trigger player death.");
        }
    }
}
