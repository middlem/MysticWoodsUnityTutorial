using System;
using UnityEngine;

public class RockWheelWeapon : Weapon
{
    float rockProjectileCurrentCount = 0;
    float rockProjectileMaxCount = 1;

    protected override void Start()
    {
        base.Start(); // Super
        this.weaponName = "Rock Wheel";
        this.weaponIcon = null;
    }

    protected override void FixedUpdate()
    {
        if(rockProjectileCurrentCount < rockProjectileMaxCount)
        {
            createProjectile();
        }
    }

    private void createProjectile()
    {
        rockProjectileCurrentCount++;
        var player_pos = GameObject.FindWithTag("Player").transform.position;
        Instantiate(my_projectile, player_pos, Quaternion.identity);
    }
}
