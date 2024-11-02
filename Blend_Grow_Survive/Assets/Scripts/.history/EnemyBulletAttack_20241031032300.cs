
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
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint; // Where bullets are spawned
    public float bulletSpeed = 10f;
    public float shootingInterval = 2f; // Time between shots
    public float spreadAngle = 15f; // Angle between the two bullets
    private float shootingTimer = 0f;

    void Update()
    {
        shootingTimer += Time.deltaTime;
        if (shootingTimer >= shootingInterval)
        {
            ShootSpread();
            shootingTimer = 0f;
        }
    }

    void ShootSpread()
    {
        // Calculate the direction towards the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;

            // Calculate the angles for two bullets with spread
            Vector2 leftBulletDirection = Quaternion.Euler(0, 0, spreadAngle) * directionToPlayer;
            Vector2 rightBulletDirection = Quaternion.Euler(0, 0, -spreadAngle) * directionToPlayer;

            // Instantiate and set up the left bullet
            GameObject leftBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            leftBullet.GetComponent<Rigidbody2D>().velocity = leftBulletDirection * bulletSpeed;

            // Instantiate and set up the right bullet
            GameObject rightBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            rightBullet.GetComponent<Rigidbody2D>().velocity = rightBulletDirection * bulletSpeed;

            Debug.Log("Boss fired dual bullets!");
        }
    }
}
