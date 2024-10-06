using UnityEngine;

public class ShootingControl : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign the Projectile prefab in the Inspector
    public float projectileSpeed = 20f; // Speed of the projectile

    void Update()
    {
        // Detect left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left mouse button clicked.");
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.Log("Shoot method called.");
        // Get the mouse position in world space
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f; // Assuming a 2D game on the XY plane

        // Determine the direction from the player to the mouse position
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;

        // Instantiate the projectile at the player's position
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Set the projectile's velocity
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }

        // Optional: Rotate the projectile to face the direction (useful for visual orientation)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
