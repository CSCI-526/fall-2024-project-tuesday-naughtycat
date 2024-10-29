using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class EnemyBulletAttack : MonoBehaviour
// {
//     public float bullet_speed = 20f;
//     public float max_distance = 20f;
//     public LayerMask player_layer_mask;

//     private Vector3 start_position;
//     private GameObject player;
//     private ObjectGenerator generator;

//     void Start()
//     {
//         start_position = transform.position;
//         generator = ObjectGenerator.ins;

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

//         RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, bullet_speed * Time.deltaTime, player_layer_mask);
//         if (hit.collider != null)
//         {
//             if (hit.collider.CompareTag("Player"))
//             {
//                 PlayerEat playerEat = hit.collider.GetComponent<PlayerEat>();
//                 if (playerEat != null)
//                 {
//                     playerEat.TakeDamage(1);
//                     if (playerEat.currentHealth <= 0)
//                     {
//                         playerEat.GameOver();
//                     }
//                 }
//                 Destroy(gameObject);
//             }
//         }
//     }
// }
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
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * bullet_speed * Time.deltaTime, Space.World);
        }

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
    }
}
