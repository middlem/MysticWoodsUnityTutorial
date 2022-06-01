using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    //Transform transform;
    public GameObject myDrop;
    public Vector3 speed = Vector3.zero;
    public bool movingUpY = true;
    public bool movingLeftX = true;
    public bool direction = false;

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

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().killCount++;
        
    }

    public void RemoveEnemy()
    {
        Vector3 deathPos = this.gameObject.transform.position;
        Destroy(gameObject);
        //GameObject prefabToSpawn = Instantiate(gameObject, deathPos, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 6;
        animator = GetComponent<Animator>();
        //transform = GetComponent<Transform>();
        speed.x = 0.0005f;
        speed.y = 0.0005f;
        
    }

    // Update is called once per frame
    void Update()
    {
        //var playerPos = myDrop.GetComponent<PlayerController>().transform.position;
        var player = GameObject.FindWithTag("Player").transform.position;
        var vectorSum = player - this.transform.position;

        Vector3 newPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        newPos.x += (vectorSum.x > 0 ? speed.x : (speed.x * -1));
        newPos.y += (vectorSum.y > 0 ? speed.y : (speed.y * -1));
        transform.position = newPos;
    }
}
