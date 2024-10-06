using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Ammo flag
    private bool hasAmmo = true;

    // Reference to the projectile prefab
    public GameObject projectilePrefab;

    // Projectile speed
    public float projectileSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        HandleShooting();
    }

    // Handle shooting input and logic
    void HandleShooting()
    {
        if (hasAmmo && Input.GetMouseButtonDown(0))
        {
            Shoot();
            hasAmmo = false; // no ammo after shooting
        }
    }

    // Method to shoot the projectile
    void Shoot()
    {
        // Get mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Calculate direction from player to mouse
        Vector2 direction = (mousePos - transform.position).normalized;

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Set the velocity of the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;

        // Optionally, set the rotation of the projectile to face the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Method to grant ammo
    public void GrantAmmo()
    {
        hasAmmo = true;
        // Optionally, add UI feedback here
    }
}

