using System;
using UnityEngine;

public class RockWheel : Projectile
{
    public float distanceFromPlayer = 0.5f;
    public float currentRotationOnPlayer = 0f;
    public float rotationSpeed = 3f;
    //float currentSpriteRotation = 0f;
    //float spriteRotationSpeed = 1f;

    protected override void Start()
    {
        base.Start(); // Super
        this.weaponDamage = 10;
    }

    void FixedUpdate()
    {
        var player = GameObject.FindWithTag("Player").transform.position;

        currentRotationOnPlayer += rotationSpeed;
        currentRotationOnPlayer = currentRotationOnPlayer % 360;

        
        Vector3 newPos = new Vector3(
            player.x + (float)Math.Cos(Math.PI * currentRotationOnPlayer / 180.0) * distanceFromPlayer,
            player.y + (float)Math.Sin(Math.PI * currentRotationOnPlayer / 180.0) * distanceFromPlayer,
            player.z);

        Quaternion newRotation = new Quaternion();
        this.transform.SetPositionAndRotation(newPos, newRotation);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Health -= weaponDamage;
                audioSource.Play();
            }
        }
        else if (other.tag == "Boss")
        {
            Boss enemy = other.GetComponent<Boss>();
            if (enemy != null)
            {
                enemy.Health -= weaponDamage;
                audioSource.Play();
                if (enemy.Health <= 0)
                {
                    enemy.RemoveMe();
                }
            }
        }
    }
}
