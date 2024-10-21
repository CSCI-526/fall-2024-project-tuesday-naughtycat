using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject player; // Reference to the player
    public GameObject bullet; // Bullet prefab
    public Transform bullet_start_point; // Bullet starting position
    public float bullet_speed = 10f; // Bullet speed
    public float shooting_interval = 2f; // Time between shots
    private float shooting_timer = 0f; // Timer to track shooting

    void Update()
    {
        shooting_timer += Time.deltaTime;

        // If the shooting timer exceeds the interval, shoot a bullet
        if (shooting_timer >= shooting_interval)
        {
            ShootAtPlayer();
            shooting_timer = 0f; // Reset the timer after shooting
        }
    }

    // This method handles the shooting logic
    void ShootAtPlayer()
    {
        if (player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Instantiate the bullet at the bullet start point and set its velocity
            GameObject bullet_instance = Instantiate(bullet, bullet_start_point.position, Quaternion.identity);
            Rigidbody2D rb = bullet_instance.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bullet_speed;
        }
    }
}
