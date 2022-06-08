using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Rigidbody2D rb;
    MovementDriver movementDriver;
    volatile bool inPushback = false; 
    Animator animator;

    public AudioSource bossAudioSource;

    public int Health = 1000;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movementDriver = new MovementDriver(rb);
        bossAudioSource = GetComponent<AudioSource>();

        //bossAudioSource.Play();

        movementDriver.MoveSpeed = 0.1f;
        movementDriver.CollisionOffset = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!inPushback)
        {
            var player = GameObject.FindWithTag("Player").transform.position;
            movementDriver.TryMoveTowardsPosition(this.transform.position, player);
        }
    }

    public void resetDamageTrigger()
    {
        animator.ResetTrigger("isTakingDamage");
    }

    public void RemoveMe()
    {
        //Vector3 deathPos = this.gameObject.transform.position;
        Destroy(gameObject);
        //GameObject prefabToSpawn = Instantiate(myDrop, deathPos, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            animator.SetTrigger("isTakingDamage");
            // Pushback
            inPushback = true;
            var player = GameObject.FindWithTag("Player").transform.position;
            movementDriver.TryMoveAwayFromPositionByDistance(this.transform.position, player, 5f);
            // Allows for a call to Update to move the object
            StartCoroutine(EndPushBack());
            
            //Projectile proj = other.GetComponent<Projectile>();
            //playerAudioSource.clip = drop.coinPickupClip;
            //playerAudioSource.Play();
        }
    }

    public IEnumerator EndPushBack()
    {
        var ret = new WaitForSeconds(0.25f);
        yield return ret;
        inPushback = false;
    }
}
