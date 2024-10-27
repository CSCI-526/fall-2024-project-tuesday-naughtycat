using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int gunIndex;
    public int maxAmmo;
    public int currentAmmo;
    public float fireRate;
    public float damage;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int directionCount;
    public float spreadAngle;
    public int bulletCount;
    public float bulletSpacing;
    public float bulletSpeed;

    private float nextTimeToFire = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Shooting behavior
    public virtual void Shoot()
    {
        Physics2D.SyncTransforms();
        if (currentAmmo > 0 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            currentAmmo -= directionCount * bulletCount;
            // Implement shooting logic like raycasting, spawning bullets, etc.
            for (int i = 0; i < directionCount; i++)
            {
                for (int j = 0; j < bulletCount; j++)
                {
                    float angle = i * spreadAngle;
                    Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, angle)) * firePoint.rotation;
                    Vector3 bulletDirection = (bulletRotation * Vector3.right).normalized;
                    Vector3 bulletOffset = firePoint.position + bulletDirection * (j - (bulletCount - 1) / 2) * bulletSpacing;
                    GameObject bullet = Instantiate(bulletPrefab, bulletOffset, bulletRotation);
                    //Rigidbody rb = bullet.GetComponent<Rigidbody>();
                    //rb.velocity = bulletRotation * Vector3.forward * bulletSpeed;
                }
            }
        }
        else if (currentAmmo <= 0)
        {
            Debug.Log(gunIndex + " is out of ammo. Reload needed.");
        }
    }

    // Reloading behavior
    public virtual void Reload()
    {
        currentAmmo = maxAmmo;
        Debug.Log(gunIndex + " reloaded.");
    }
}
