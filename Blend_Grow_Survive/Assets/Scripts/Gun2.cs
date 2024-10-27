using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun2 : Gun
{
    // Start is called before the first frame update
    void Start()
    {
        gunIndex = 1;
        maxAmmo = 100;
        currentAmmo = maxAmmo;
        fireRate = 1f;
        damage = 10f;
        directionCount = 2;
        spreadAngle = 180f;
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
        base.Shoot();
        // Additional pistol-specific logic if needed
    }
}
