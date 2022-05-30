using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public GameObject myDrop;

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

    public float health = 1;

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy()
    {
        Vector3 deathPos = this.gameObject.transform.position;
        Destroy(gameObject);
        GameObject prefabToSpawn = Instantiate(myDrop, deathPos, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
    animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
