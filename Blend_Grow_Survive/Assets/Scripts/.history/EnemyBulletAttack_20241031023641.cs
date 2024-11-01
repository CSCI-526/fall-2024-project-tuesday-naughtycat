// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyBulletAttack : MonoBehaviour
// {
//     public float bullet_speed = 20f;
//     public float max_distance = 20f;
//     public LayerMask player_layer_mask;

//     private Vector3 start_position;
//     private GameObject player;

//     void Start()
//     {
//         start_position = transform.position;
//         player = GameObject.FindGameObjectWithTag("Player");
//     }

//     void Update()
//     {
//         if (player != null)
//         {
//             Vector2 direction = (player.transform.position - transform.position).normalized;
//             transform.Translate(direction * bullet_speed * Time.deltaTime, Space.World);
//         }

//         if (Vector3.Distance(start_position, transform.position) >= max_distance)
//         {
//             Destroy(gameObject);
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.CompareTag("Player"))
//         {
//             PlayerEat playerEat = collision.GetComponent<PlayerEat>();
//             if (playerEat != null)
//             {
//                 playerEat.TakeDamage(1); // Reduce player health
//             }
//             Destroy(gameObject); // Destroy the bullet immediately after hitting the player
//         }

//         // Check for collision with Wall
//         else if (collision.CompareTag("Wall"))
//         {
//             Debug.Log("Bullet hit the wall and got destroyed.");
//             Destroy(gameObject);
//         }
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletAttack : MonoBehaviour
{
    public float bullet_speed = 20f;
    public float max_distance = 20f;
    public LayerMask player_layer_mask;

    private Vector3 start_position;
    private Vector2 bullet_direction; // Store initial direction
    private GameObject player;

    void Start()
    {
        start_position = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Calculate the direction once, from the bullet's initial position to the player's position
            bullet_direction = (player.transform.position - transform.position).normalized;
        }
        else
        {
            // If there's no player, set the bullet direction to zero to avoid movement issues
            bullet_direction = Vector2.zero;
        }
    }

    void Update()
    {
        // Move the bullet in the stored direction
        transform.Translate(bullet_direction * bullet_speed * Time.deltaTime, Space.World);

        // Destroy the bullet if it exceeds the maximum distance
        if (Vector3.Distance(start_position, transform.position) >= max_distance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerEat playerEat = collision.GetComponent<PlayerEat>();
            if (playerEat != null)
            {
                playerEat.TakeDamage(1); // Reduce player health
            }
            Destroy(gameObject); // Destroy the bullet immediately after hitting the player
        }
        else if (collision.CompareTag("Wall"))
        {
            Debug.Log("Bullet hit the wall and got destroyed.");
            Destroy(gameObject);
        }
    }
}
