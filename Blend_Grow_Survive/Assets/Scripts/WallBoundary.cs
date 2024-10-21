using UnityEngine;

public class WallBoundary : MonoBehaviour
{
    private PlayerMovements playerMovement;

    void Start()
    {
        // Get the PlayerMovements component attached to the player
        playerMovement = GetComponent<PlayerMovements>();
    }

    // Detect collision with the wall
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collided with a GameObject tagged as "Wall"
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Stop the player's movement by calling StopMovement()
            playerMovement.StopMovement();
        }
    }

    // If the player exits the wall
    void OnCollisionExit2D(Collision2D collision)
    {
        // Ensure the player can move again after leaving the wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            playerMovement.ResumeMovement();
        }
    }
}