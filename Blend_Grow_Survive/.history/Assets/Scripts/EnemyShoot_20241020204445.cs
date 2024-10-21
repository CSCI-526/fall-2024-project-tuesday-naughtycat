using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject player; // Reference to the player
    public GameObject bullet; // Bullet prefab
    public float bullet_speed = 10f; // Bullet speed
    public float shooting_interval = 2f; // Time between shots
    private float shooting_timer = 0f; // Timer to track shooting

    void Update()
    {
        shooting_timer += Time.deltaTime;

        // Automatically shoot every 'shooting_interval' seconds
        if (shooting_timer >= shooting_interval)
        {
            ShootAtPlayer();
            shooting_timer = 0f; // Reset timer after shooting
        }
    }

    // This method handles shooting at the player's current position
    void ShootAtPlayer()
    {
        if (player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Instantiate the bullet at the enemy's center (transform.position) and set its velocity
            GameObject bullet_instance = Instantiate(bullet, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet_instance.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bullet_speed;

            // Optional: rotate the bullet to face the player (if the bullet sprite needs to face the player)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet_instance.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}


