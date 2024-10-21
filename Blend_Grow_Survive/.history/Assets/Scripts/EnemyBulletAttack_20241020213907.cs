using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletAttack : MonoBehaviour
{
    public float bullet_speed = 20f;  // Speed of the bullet
    public float max_distance = 20f;  // Maximum distance before the bullet is destroyed
    public LayerMask player_layer_mask;  // Mask to detect the player

    private Vector3 start_position;
    private GameObject player;  // Reference to the player object
    private ObjectGenerator generator;

    void Start()
    {
        start_position = transform.position;
        generator = ObjectGenerator.ins;  // Reference to ObjectGenerator for managing game objects

        // Find the player by tag (make sure the player has the "Player" tag)
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Move the bullet towards the player's current position
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * bullet_speed * Time.deltaTime, Space.World);
        }

        // Destroy the bullet if it has traveled the maximum distance
        if (Vector3.Distance(start_position, transform.position) >= max_distance)
        {
            Destroy(gameObject);
        }

        // Raycast to detect if the bullet hits the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, bullet_speed * Time.deltaTime, player_layer_mask);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))  // Check if the object hit is the player
            {
                PlayerEat playerEat = hit.collider.GetComponent<PlayerEat>();
                if (playerEat != null)
                {
                    playerEat.TakeDamage(1);  // Deal damage to the player

                    if (playerEat.currentHealth <= 0)
                    {
                        playerEat.GameOver();  // End the game if the player's health reaches 0
                    }
                }

                // Destroy the bullet after hitting the player
                Destroy(gameObject);
            }
        }
    }
}
