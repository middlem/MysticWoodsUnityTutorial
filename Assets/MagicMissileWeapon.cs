using UnityEngine;
using System.Collections;

public class MagicMissileWeapon : Weapon
{
    public float attack_speed = 1;

    protected override void Start()
    {
        base.Start(); // Super
        this.weaponName = "Magic Missile";
        this.weaponIcon = null;
        this.maxWeaponLevel = 2;
        this.upgradeDescriptions.Add(1, "Fires off a missile towards a random enemy");
        this.upgradeDescriptions.Add(2, "*25% Incrased Attack Speed");

        StartCoroutine(createProjectile());
    }

    protected override void FixedUpdate()
    {

    }

    public override void upgradeWeapon()
    {
        base.upgradeWeapon();
        if (weaponLevel == 2)
        {
            attack_speed *= 0.75f;
            Debug.Log("MagicMissileWeapon AS: " + attack_speed);
        }
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
