using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amogus : Projectile
{
    public float projectileSpeed = 0.02f;
    public float collisionOffset = 0.05f;
    Rigidbody2D rb;
    public int collisionMaxCount = 3;
    private int collisionCount = 0;

    private float distanceTravelled = 0f;
    private float maxDistance = 10f;

    // true = right
    // false = left
    public bool go_right = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var player_obj = GameObject.FindWithTag("Player");
        PlayerController player = player_obj.GetComponent<PlayerController>();
        go_right = player.playerDirection == PlayerController.Player_Direction.Right ? true : false;
        this.weaponDamage = 10;
    }

    private void FixedUpdate()
    {
        if (distanceTravelled < maxDistance)
        {
            float newX = go_right ? rb.position.x + projectileSpeed : rb.position.x - projectileSpeed;
            distanceTravelled += projectileSpeed;
            Vector2 nextPosition = new Vector2(newX, rb.position.y);
            rb.MovePosition(nextPosition);
        }
        else
        {
            //Remove Prefab when offscreen
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Health -= weaponDamage;
                collisionCount++;
                if (audioSource != null)
                    audioSource.Play();
                if (collisionCount >= collisionMaxCount)
                    Destroy(gameObject);
            }
        }
        else if (other.tag == "Boss")
        {
            Boss enemy = other.GetComponent<Boss>();
            if (enemy != null)
            {
                enemy.Health -= weaponDamage;
                Debug.Log("Morbin health: " + enemy.Health);
                if (audioSource != null)
                    audioSource.Play();
                if (enemy.Health <= 0)
                {
                    enemy.RemoveMe();
                }
                collisionCount++; 
            }
        }
    }

    private float GetCameraXMax()
    {
        return Camera.main.orthographicSize * Screen.width / Screen.height;
    }
}
