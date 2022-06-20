using UnityEngine;
using System.Collections;

public class AmogunWeapon : Weapon
{
    public float attack_speed = 1f;
    public float num_projectiles = 1;
    public float projectile_spawn_radius = 0.4f; // the range from the center of player to which projectiles can spawn
    private float center_of_player = -0.1f; // offset for what looks to be center of player

    protected override void Start()
    {
        base.Start(); // Super
        this.weaponName = "Amogun";
        this.weaponIcon = null;
        this.maxWeaponLevel = 2;
        this.upgradeDescriptions.Add(1, "Shoots something a little sus in the facing direction of the player");
        this.upgradeDescriptions.Add(2, "+1 additional projectile");


        StartCoroutine(createProjectiles());
    }

    protected override void FixedUpdate()
    {

    }

    public IEnumerator createProjectiles()
    {
        while (true)
        {
            var spawn_width = (projectile_spawn_radius * 2) / (num_projectiles+1);
            for(int projectile_count = 1; projectile_count <= num_projectiles; projectile_count++)
            {
                // 0.4 to -0.4
                SpawnProjectile(center_of_player - projectile_spawn_radius + (spawn_width * projectile_count));
            }
            
            
            yield return new WaitForSeconds(attack_speed);
        }
    }

    public override void upgradeWeapon()
    {
        base.upgradeWeapon();
        if (weaponLevel == 2)
        {
            num_projectiles++;
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
