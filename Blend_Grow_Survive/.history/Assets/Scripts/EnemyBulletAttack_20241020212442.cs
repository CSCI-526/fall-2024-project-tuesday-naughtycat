using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletAttack : MonoBehaviour
{
    public float bullet_speed = 20f;  // Speed of the bullet
    public float max_distance = 20f;  // Maximum distance before the bullet is destroyed
    public LayerMask player_layer_mask;  // Mask to detect the player

    private Vector3 start_position;
    ObjectGenerator generator;

    void Start()
    {
        start_position = transform.position;
        generator = ObjectGenerator.ins;  // Reference to ObjectGenerator for managing game objects
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.up * bullet_speed * Time.deltaTime);

        // Destroy the bullet if it has traveled the maximum distance
        if (Vector3.Distance(start_position, transform.position) >= max_distance)
        {
            Destroy(gameObject);
        }

        // Raycast to detect if the bullet hits the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, bullet_speed * Time.deltaTime, player_layer_mask);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))  // Check if the object hit is the player
            {
                PlayerEat player = hit.collider.GetComponent<PlayerEat>();
                if (player != null)
                {
                    player.TakeDamage(1);  // Deal damage to the player

                    if (player.currentHealth <= 0)
                    {
                        player.GameOver();  // End the game if the player's health reaches 0
                    }
                }

                // Destroy the bullet after hitting the player
                Destroy(gameObject);
            }
        }
    }
}
