using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public float collisionOffset = 0.01f;
    public float moveSpeed = 0.2f;
    public GameObject myDrop;
    public bool movingUpY = true;
    public bool movingLeftX = true;
    public bool direction = false;
    public int damage = 1;

    Rigidbody2D rb;

    MovementDriver movementDriver;

    public float Health
    {
        set
        {
            health = value;
            if(health <= 0)
            {
                Defeated();
            }
        }

        get
        {
            return health;
        }
    }

    public float health = 6;

    // Start is called before the first frame update
    void Start()
    {
        health = 6;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        movementDriver = new MovementDriver(rb);
        movementDriver.MoveSpeed = moveSpeed;
        movementDriver.CollisionOffset = collisionOffset;
    }

    public void Defeated()
    {
        movementDriver.LockMovement();
        animator.SetTrigger("Defeated");
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().killCount++;
        Vector3 deathPos = this.gameObject.transform.position;
        GameObject prefabToSpawn = Instantiate(myDrop, deathPos, Quaternion.identity);
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindWithTag("Player").transform.position;
        movementDriver.TryMoveTowardsPosition(this.transform.position, player);
    }
}
