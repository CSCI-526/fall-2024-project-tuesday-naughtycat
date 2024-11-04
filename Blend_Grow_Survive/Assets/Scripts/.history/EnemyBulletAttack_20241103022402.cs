
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
