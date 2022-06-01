using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 1f;
    public float collisionOffset = 0.05f;
    Rigidbody2D rb;
    public int collisionMaxCount = 3;
    private int collisionCount = 0;

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
        
        float horizon = GetCameraXMax();
        if (rb.position.x > -horizon && rb.position.x < horizon)
        {
            float newX = direction ? rb.position.x + projectileSpeed : rb.position.x - projectileSpeed;
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
    }

    private float GetCameraXMax()
    {
        return Camera.main.orthographicSize * Screen.width / Screen.height;
    }
}
