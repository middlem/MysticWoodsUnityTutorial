using System.Collections.Generic;
using UnityEngine;

public class RockWheelWeapon : Weapon
{
    float rockProjectileCurrentCount = 0;
    float rockProjectileMaxCount = 1;
    public List<RockWheel> rockProjectiles;

    protected override void Start()
    {
        base.Start(); // Super
        this.weaponName = "Rock Wheel";
        this.weaponIcon = null;
        this.maxWeaponLevel = 2;
        this.upgradeDescriptions.Add(1, "Creates a rotating rock that circles around the player damaging enemies it collides with");
        this.upgradeDescriptions.Add(2, "+1 Rock");
    }

    protected override void FixedUpdate()
    {
        if(rockProjectileCurrentCount < rockProjectileMaxCount)
        {
            createProjectile();
            balanceProjectiles();
        }
    }

    public override void upgradeWeapon()
    {
        base.upgradeWeapon();
        if(weaponLevel == 2)
        {
            rockProjectileMaxCount++;
        }
    }

    private void balanceProjectiles()
    {
        if(rockProjectiles.Count > 1)
        {
            float playerRotationDistance = 360 / rockProjectiles.Count;
            for(int projectileNum = 0; projectileNum < rockProjectiles.Count; projectileNum++)
            {
                rockProjectiles[projectileNum].currentRotationOnPlayer = playerRotationDistance * projectileNum;
            }
        }
    }

    private void createProjectile()
    {
        rockProjectileCurrentCount++;
        var player_pos = GameObject.FindWithTag("Player").transform.position;
        var newProjectile = Instantiate(my_projectile, player_pos, Quaternion.identity);
        rockProjectiles.Add(newProjectile.GetComponent<RockWheel>());
    }
}
