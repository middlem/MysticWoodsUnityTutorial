using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    Vector2 rightAttackOffset;
    public Collider2D swordCollider;
    public AudioSource swordAudioClip;
    public AudioClip swordMissClip;
    public AudioClip swordHitClip;

    public float damage = 3;

    // Start is called before the first frame update
    void Start()
    {
        rightAttackOffset = transform.position;
        swordAudioClip = GetComponent<AudioSource>();
    }

 
    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
        swordAudioClip.clip = swordMissClip;
        swordAudioClip.Play();
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x = -1, rightAttackOffset.y);
        swordAudioClip.clip = swordMissClip;
        swordAudioClip.Play();
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            swordAudioClip.clip = swordHitClip;
            swordAudioClip.Play();
            if (enemy != null)
            {
                enemy.Health -= damage;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
