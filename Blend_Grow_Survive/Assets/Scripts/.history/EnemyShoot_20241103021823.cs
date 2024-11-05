using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject EnemyBullet;
    public float bullet_speed = 10f;
    public float shooting_interval = 5f;
    private float shooting_timer = 0f;

    // Check if this enemy is the boss
    private bool canShoot;

    void Start()
    {
        // Allow shooting for Boss and EnemyArcher
        canShoot = gameObject.CompareTag("Boss") || gameObject.CompareTag("EnemyArcher");
    }

    void Update()
    {
        // Only allow the boss to shoot
        if (canShoot)
        {
            shooting_timer += Time.deltaTime;
            if (shooting_timer >= shooting_interval)
            {
                ShootAtPlayer();
                shooting_timer = 0f;
            }
        }
    }

    void ShootAtPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            GameObject EnemyBullet_instance = Instantiate(EnemyBullet, transform.position, Quaternion.identity);
            Rigidbody2D rb = EnemyBullet_instance.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bullet_speed;

            // Rotate EnemyBullet to face player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            EnemyBullet_instance.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
