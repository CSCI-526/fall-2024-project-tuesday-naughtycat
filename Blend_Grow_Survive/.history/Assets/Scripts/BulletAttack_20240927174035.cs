using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet collides with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);  // Destroy the enemy
            Destroy(gameObject);  // Destroy the bullet itself
        }
        else
        {
            Destroy(gameObject);  // Destroy the bullet if it hits something else
        }
    }
}
