
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public float bulletSpeed = 20f;  // Speed of the bullet
    public float maxDistance = 20f;  // Maximum distance the bullet can travel
    public LayerMask enemyLayerMask; // Assign the layer for enemies in the Inspector

    private Vector3 startPosition;  // To track where the bullet was spawned

    void Start()
    {
        startPosition = transform.position;  // Record the start position when the bullet is spawned
    }

    void Update()
    {
        // Move the bullet forward in a straight line
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);

        // Check if the bullet has traveled the max distance
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);  // Destroy the bullet after it travels 20 units
        }

        // Raycast to detect if the bullet is close to an enemy
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, bulletSpeed * Time.deltaTime, enemyLayerMask);
        if (hit.collider != null)
        {
            // Check if the ray hit an enemy
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);  // Destroy the enemy
                Destroy(gameObject);  // Destroy the bullet itself
            }
        }
    }
}
