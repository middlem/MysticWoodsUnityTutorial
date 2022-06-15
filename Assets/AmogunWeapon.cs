using UnityEngine;
using System;
using System.Collections;

public class AmogunWeapon : Weapon
{
    public float attack_speed = 1f;

    protected override void Start()
    {
        base.Start(); // Super
        this.weaponName = "Amogun";
        this.weaponIcon = null;
        StartCoroutine(createProjectiles());
    }

    protected override void FixedUpdate()
    {

    }

    public IEnumerator createProjectiles()
    {
        while (true)
        {
            SpawnProjectile(-0.1f); // center of player?
            
            yield return new WaitForSeconds(attack_speed);
        }
    }

    public void SpawnProjectile(float yOffsetValue = 0)
    {
        var player_obj = GameObject.FindWithTag("Player");
        PlayerController player = player_obj.GetComponent<PlayerController>();
        bool go_right = player.playerDirection == PlayerController.Player_Direction.Right ? true : false;

        var player_pos = player_obj.transform.position;

        float distance = go_right ? 0.2f : -0.2f;
        Vector3 myPos = new Vector3(player_pos.x + distance, player_pos.y + yOffsetValue, player_pos.z);
        GameObject weapon = Instantiate(my_projectile, myPos, Quaternion.identity);
    }
}
