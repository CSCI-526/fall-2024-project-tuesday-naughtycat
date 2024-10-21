using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public GameObject bullet;
    public float bullet_speed = 10f;
    public float shooting_interval = 5f;
    private float shooting_timer = 0f;

    void Update()
    {
        shooting_timer += Time.deltaTime;
        if (shooting_timer >= shooting_interval)
        {
            ShootAtPlayer();
            shooting_timer = 0f;
        }
    }
    void ShootAtPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            GameObject bullet_instance = Instantiate(bullet, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet_instance.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bullet_speed;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet_instance.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}


