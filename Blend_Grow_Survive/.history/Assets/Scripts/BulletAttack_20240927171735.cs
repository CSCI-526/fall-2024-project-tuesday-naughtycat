using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Log what the bullet collides with
        Debug.Log("Bullet collided with: " + collision.gameObject.name);

        // Check if the collided object is tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the enemy
            Debug.Log("Enemy hit, destroying enemy");
            Destroy(collision.gameObject);

            // Destroy the bullet
            Debug.Log("Destroying bullet");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Collided with non-enemy object.");
        }
    }
}
