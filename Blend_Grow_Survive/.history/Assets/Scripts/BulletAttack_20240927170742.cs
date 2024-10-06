using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    // This will be called when the bullet collides with another object
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is the enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the enemy
            Destroy(collision.gameObject);

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
