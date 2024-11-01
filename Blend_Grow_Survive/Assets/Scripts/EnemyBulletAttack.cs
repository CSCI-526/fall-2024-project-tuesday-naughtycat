
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyBulletAttack : MonoBehaviour
// {
//     public float bullet_speed = 20f;
//     public float max_distance = 20f;
//     public float turnSpeed = 2f; // Speed at which the bullet turns toward the player
//     public LayerMask player_layer_mask;

//     private Vector3 start_position;
//     private Vector2 current_direction;
//     private GameObject player;

//     void Start()
//     {
//         start_position = transform.position;
//         player = GameObject.FindGameObjectWithTag("Player");

//         if (player != null)
//         {
//             current_direction = (player.transform.position - transform.position).normalized;
//         }
//     }

//     void Update()
//     {
//         if (player != null)
//         {
//             // Calculate the desired direction towards the player's current position
//             Vector2 targetDirection = (player.transform.position - transform.position).normalized;

//             // Gradually rotate the current direction towards the target direction
//             current_direction = Vector2.Lerp(current_direction, targetDirection, turnSpeed * Time.deltaTime).normalized;
//         }

//         // Move the bullet in the gradually updated direction
//         transform.Translate(current_direction * bullet_speed * Time.deltaTime, Space.World);

//         // Destroy the bullet if it exceeds the maximum distance
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
//         else if (collision.CompareTag("Wall"))
//         {
//             Debug.Log("Bullet hit the wall and got destroyed.");
//             Destroy(gameObject);
//         }
//     }
// }


using System.Collections;
using UnityEngine;

public class EnemyBulletAttack : MonoBehaviour
{
    public float bullet_speed = 20f;
    public float max_distance = 20f;
    public LayerMask player_layer_mask;

    private Vector3 start_position;
    private GameObject player;

    void Start()
    {
        start_position = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");

        Vector2 direction = (player.transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * bullet_speed;
    }

    void Update()
    {
        if (Vector3.Distance(start_position, transform.position) >= max_distance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // Reduce player health by 1 on hit
            }
            Destroy(gameObject); // Destroy the bullet after hitting the player
        }
        else if (collision.CompareTag("Wall"))
        {
            Debug.Log("Bullet hit the wall and got destroyed.");
            Destroy(gameObject);
        }
    }
}
