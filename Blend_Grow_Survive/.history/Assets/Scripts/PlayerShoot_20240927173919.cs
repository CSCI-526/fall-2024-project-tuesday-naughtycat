// using UnityEngine;

// public class PlayerShoot : MonoBehaviour
// {
//     // Assign this in the Inspector (drag your bullet prefab here)
//     public GameObject bulletPrefab;

//     // Set bullet speed in the Inspector
//     public float bulletSpeed = 10f;

//     // Bullet spawn offset (so the bullet appears in front of the player)
//     public Vector2 bulletOffset = new Vector2(0.5f, 0);

//     void Update()
//     {
//         // Detect left mouse button click
//         if (Input.GetMouseButtonDown(0))
//         {
//             Shoot();
//         }
//     }

//     void Shoot()
//     {
//         // Calculate the bullet spawn position (in front of the player)
//         Vector2 spawnPosition = (Vector2)transform.position + bulletOffset;

//         // Instantiate bullet
//         GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

//         // Ensure the bullet has a Rigidbody2D component
//         Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
//         if (rb != null)
//         {
//             // Get the direction from the player to the mouse position
//             Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//             Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

//             // Apply velocity to the bullet
//             rb.velocity = direction * bulletSpeed;
//         }
//         else
//         {
//             Debug.LogWarning("The bulletPrefab does not have a Rigidbody2D component attached!");
//         }
//     }
// }
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;  // Drag your bullet prefab here
    public Transform bulletSpawnPoint;  // The point where the bullet will be spawned
    public float bulletSpeed = 20f;  // Speed of the bullet
    private bool hasShot = false;  // To keep track if the player has already shot

    void Update()
    {
        // Check if the left mouse button (0 is the left button) is pressed and the player hasn't shot yet
        if (Input.GetMouseButtonDown(0) && !hasShot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the bullet and give it velocity
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bulletSpawnPoint.up * bulletSpeed;

        // Mark that the player has already shot
        hasShot = true;
    }
}
