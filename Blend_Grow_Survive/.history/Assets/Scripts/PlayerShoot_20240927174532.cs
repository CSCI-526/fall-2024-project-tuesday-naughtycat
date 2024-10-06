
// using UnityEngine;

// public class PlayerShoot : MonoBehaviour
// {
//     public GameObject bulletPrefab;  // Drag your bullet prefab here
//     public Transform bulletSpawnPoint;  // The point where the bullet will be spawned
//     public float bulletSpeed = 20f;  // Speed of the bullet
//     private bool hasShot = false;  // To keep track if the player has already shot

//     void Update()
//     {
//         // Check if the left mouse button (0 is the left button) is pressed and the player hasn't shot yet
//         if (Input.GetMouseButtonDown(0) && !hasShot)
//         {
//             Shoot();
//         }
//     }

//     void Shoot()
//     {
//         // Instantiate the bullet and give it velocity
//         GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
//         Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
//         rb.velocity = bulletSpawnPoint.up * bulletSpeed;

//         // Mark that the player has already shot
//         hasShot = true;
//     }
// }

using UnityEngine;

public class PlayerShooting : MonoBehaviour
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
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the bullet spawn point to the mouse position
        Vector2 direction = (mousePosition - bulletSpawnPoint.position).normalized;

        // Instantiate the bullet at the spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // Get the bullet's Rigidbody2D component and set its velocity to the direction
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;

        // Mark that the player has already shot
        hasShot = true;
    }
}
