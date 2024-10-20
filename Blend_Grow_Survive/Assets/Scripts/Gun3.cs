using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun3 : Gun
{
    // Start is called before the first frame update
    void Start()
    {
        gunIndex = 2;
        maxAmmo = 100;
        currentAmmo = maxAmmo;
        fireRate = 1f;
        damage = 10f;
        directionCount = 4;
        spreadAngle = 90f;
        bulletCount = 1;
        bulletSpacing = 0f;
        bulletSpeed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}