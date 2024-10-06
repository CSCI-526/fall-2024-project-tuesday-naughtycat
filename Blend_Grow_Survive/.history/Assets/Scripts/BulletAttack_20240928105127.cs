using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public float bulletSpeed = 20f;  // Speed of the bullet
    public float maxDistance = 20f;  // Maximum distance the bullet can travel

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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
    Debug.Log("Collision detected with: " + collision.gameObject.name);

    if (collision.gameObject.CompareTag("Enemy"))
    {
        Debug.Log("Enemy hit!");
        Destroy(collision.gameObject);  // Destroy the enemy
        Destroy(gameObject);  // Destroy the bullet
    }
    else
    {
        Destroy(gameObject);  // Destroy the bullet if it hits something else
    }
    }

}
