using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
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
    public bool direction = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (distanceTravelled < maxDistance)
        {
            float newX = direction ? rb.position.x + projectileSpeed : rb.position.x - projectileSpeed;
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
                enemy.Health -= 10;
                collisionCount++;
                if(collisionCount >= collisionMaxCount)
                    Destroy(gameObject);
            }
        }
        else if (other.tag == "Boss")
        {
            Boss enemy = other.GetComponent<Boss>();
            if (enemy != null)
            {
                enemy.Health -= 10;
                Debug.Log("Morbin health: " + enemy.Health);
                if(enemy.Health <= 0)
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
