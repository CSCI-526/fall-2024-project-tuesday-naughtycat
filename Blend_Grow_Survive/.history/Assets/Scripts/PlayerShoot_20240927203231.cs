
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public GameObject bulletPrefab; // Assign the bullet prefab in the inspector
    public Transform bulletSpawnPoint; // The position where the bullet spawns (usually in front of the player)
    public float bulletSpeed = 20f;

    private bool hasShot = false; // To ensure player can only shoot once
    private Vector2 mousePosition;
    private Vector2 direction;

    void Update()
    {
        HandleGunRotation();
        if (Input.GetMouseButtonDown(0) && !hasShot) // Left mouse click and player hasn't shot yet
        {
            Shoot();
        }
    }

    void HandleGunRotation()
    {
        // Rotate player or gun towards the mouse cursor
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePosition - (Vector2)player.transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        // Use 'transform.up' instead of 'transform.right' to point upwards toward the mouse position
        player.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f); // Subtract 90 degrees to fix the offset
    }

    void Shoot()
    {
        // Get the mouse position in the world
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse
        direction = (mousePosition - (Vector2)player.transform.position).normalized;

        // Instantiate the bullet (store in a local variable)
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, player.transform.rotation);

        // Set the bullet's velocity in the direction of the mouse
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;

        // Set hasShot to true to prevent further shooting (if desired)
        hasShot = true;

        // Optional: Add logic to reset 'hasShot' after a cooldown or when allowed to shoot again
    }
}
