using UnityEngine;
using System;
using System.Collections;

public class MagicMissileWeapon : Weapon
{
    public float attack_speed = 1;
    public int fixedDeltaCounter = 0;

    protected override void Start()
    {
        base.Start(); // Super
        this.weaponName = "Magic Missile";
        this.weaponIcon = null;
        StartCoroutine(createProjectile());
    }

    protected override void FixedUpdate()
    {

    }

    public IEnumerator createProjectile()
    {
        while (true)
        {
            var player_pos = GameObject.FindWithTag("Player").transform.position;
            Instantiate(my_projectile, player_pos, Quaternion.identity);

            yield return new WaitForSeconds(attack_speed);
        }
    }
}
