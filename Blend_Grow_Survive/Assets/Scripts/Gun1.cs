using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun1 : Gun
{
    // Start is called before the first frame update
    void Start()
    {
        gunIndex = 1;
        maxAmmo = 100;
        currentAmmo = maxAmmo;
        fireRate = 1f;
        damage = 10f;
        directionCount = 1;
        spreadAngle = 0f;
        bulletCount = 1;
        bulletSpacing = 0f;
        bulletSpeed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Shoot()
    {
        Debug.Log("Before shooting: Current ammo is " + currentAmmo);
        base.Shoot();
        // Additional pistol-specific logic if needed
    }
}
