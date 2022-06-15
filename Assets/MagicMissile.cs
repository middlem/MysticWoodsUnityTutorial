using UnityEngine;
using System;

public class MagicMissile : Projectile
{
    public float move_speed = 0.01f;
    private float my_move_angle = -1;
    public int collisionMaxCount = 1;
    private int collisionCount = 0;

    private float distanceTravelled = 0f;
    private float maxDistance = 10f;

    protected override void Start()
    {
        base.Start(); // Super
        this.weaponDamage = 10;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (distanceTravelled >= maxDistance)
        {
            Destroy(gameObject);
            return;
        }
        
        var random_enemy = GameObject.FindWithTag("Enemy").transform.position;
        Vector3 newPos;
        var my_pos = this.transform.position;

        if (my_move_angle < 0)
        {
            if (random_enemy != null)
            {
                // move to enemy instead
                my_move_angle = UnityEngine.Random.Range(0, 360);
            }
            else
            {
                my_move_angle = UnityEngine.Random.Range(0, 360);
            }
        }

        //Debug.Log("my_move_angle: " + my_move_angle);
        //Debug.Log((float)Math.Cos(Math.PI * 90 / 180.0) +", "+Math.Sin(Math.PI * 90 / 180.0));
        newPos = new Vector3(
            my_pos.x + ((float)Math.Cos(Math.PI * my_move_angle / 180.0)) * move_speed,
            my_pos.y + ((float)Math.Sin(Math.PI * my_move_angle / 180.0)) * move_speed,
            my_pos.z);

        Quaternion newRotation = Quaternion.Euler(0, 0, my_move_angle - 90);
        this.transform.SetPositionAndRotation(newPos, newRotation);
        distanceTravelled += move_speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Health -= weaponDamage;
                if(audioSource != null)
                    audioSource.Play();

                collisionCount++;
                if (collisionCount >= collisionMaxCount)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (other.tag == "Boss")
        {
            Boss enemy = other.GetComponent<Boss>();
            if (enemy != null)
            {
                enemy.Health -= weaponDamage;
                if(audioSource != null)
                    audioSource.Play();
                if (enemy.Health <= 0)
                {
                    enemy.RemoveMe();
                }
            }
        }


        
    }
}
