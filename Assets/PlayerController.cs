using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    public GameObject swordProjectile;
    public GameObject playerObject;
    public GameObject enemy;
    public GameObject bossObject;
    public bool projPickedUp = false;
    public int killCount = 0;
    public int maxHp = 100;
    public int currentHp = 100;

    public int currentXp = 0;
    public int nextLevelXp = 100;
    public AudioClip levelUpClip;

    public bool bossSpawned = false;

    public AudioSource playerAudioSource;

    [SerializeField] HealthBar healthBar;
    [SerializeField] ExperienceBar xpBar;

    Vector2 movementInput;
    Rigidbody2D rb;

    Animator animator;
    SpriteRenderer spriteRenderer;
    MovementDriver movementDriver;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnExtraProj());
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAudioSource = GetComponent<AudioSource>();

        movementDriver = new MovementDriver(rb);
        movementDriver.MoveSpeed = moveSpeed;
        movementDriver.CollisionOffset = collisionOffset;
    }

    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        currentHp -= damage;
        healthBar.SetState(currentHp, maxHp);
        Debug.Log("Current hp: " + currentHp);

        if (currentHp <= 0)
        {
            Debug.Log("Player Dead");
        }
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = movementDriver.TryMove(movementInput);

            if (!success)
            {
                success = movementDriver.TryMove(new Vector2(movementInput.x, 0)); 
            }

            if (!success)
            {
                success = movementDriver.TryMove(new Vector2(0, movementInput.y));
            }

            animator.SetBool("isMoving", success);
        } else
        {
            animator.SetBool("isMoving", false);
        }

        // Sprite direction set to movement direction
        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    // For events like on attack
    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack()
    {
        //LockMovement();
        if(spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        } else
        {
            swordAttack.AttackRight();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Drop")
        {
            Coin drop = other.GetComponent<Coin>();
            playerAudioSource.clip = drop.coinPickupClip;
            playerAudioSource.Play();
            projPickedUp = true;
            drop.RemoveCoin();
        }
        else if (other.tag == "XpDrop")
        {
            XpDrop drop = other.GetComponent<XpDrop>();
            playerAudioSource.clip = drop.pickupClip;
            playerAudioSource.Play();
            currentXp += drop.xpValue;
            if (currentXp >= nextLevelXp)
            {
                Debug.Log("Player Level Up!!");
                // This will clash with pickup and other sounds from this audio source
                playerAudioSource.clip = levelUpClip;
                playerAudioSource.Play();
                nextLevelXp = (int)(nextLevelXp * 1.2);
                currentXp = 0;
            }
            xpBar.UpdateExperienceBar(currentXp, nextLevelXp);
            drop.RemoveDrop();
        }
        else if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            currentHp -= enemy.damage;
            healthBar.SetState(currentHp, maxHp);
        }
    }

    public void SpawnProjectile(float yOffsetValue = 0)
    {
        Debug.Log("Kill Count: "+killCount);
        if (!projPickedUp) return;
        float distance = spriteRenderer.flipX ? -0.2f : 0.2f;
        Vector3 myPos = new Vector3(this.gameObject.transform.position.x + distance, this.gameObject.transform.position.y+yOffsetValue, this.gameObject.transform.position.z);
        GameObject weapon = Instantiate(swordProjectile, myPos, Quaternion.identity);
        var weaponObj = weapon.GetComponent<Projectile>();
        weaponObj.direction = !spriteRenderer.flipX;
        if (killCount > 10)
            weaponObj.projectileSpeed *= 2;
        if (killCount > 50)
            weaponObj.collisionMaxCount = 5;
        if (killCount > 100)
        {
            GameObject weapon2 = Instantiate(swordProjectile, myPos, Quaternion.identity);
            var weaponObj2 = weapon2.GetComponent<Projectile>();
            weaponObj2.direction = !weaponObj.direction;
            weaponObj2.projectileSpeed *= 2;
            weaponObj2.collisionMaxCount = 50;
            weaponObj.collisionMaxCount = 50;
        }

    }

    public IEnumerator SpawnEnemy()
    {   while (true)
        {
            Vector3 spawnPos = new Vector3(this.gameObject.transform.position.x + (Random.Range(-1.5f, 1.5f)),
                                           this.gameObject.transform.position.y + (Random.Range(-1.5f, 1.5f)),
                                           this.gameObject.transform.position.z);
            
            Instantiate(enemy, spawnPos, Quaternion.identity);

            if (killCount > 200 && bossSpawned == false)
            {
                bossSpawned = true;
                SpawnBoss();
            }

            yield return new WaitForSeconds(killCount < 50 ? 1.0f : .25f);
        }
    }

    public void SpawnBoss()
    {
        Vector3 spawnPos = new Vector3(this.gameObject.transform.position.x + (Random.Range(-3f, 3f)),
                                           this.gameObject.transform.position.y + (Random.Range(-3f, 3f)),
                                           this.gameObject.transform.position.z);

        Instantiate(bossObject, spawnPos, Quaternion.identity);
    }

    public IEnumerator SpawnExtraProj()
    {
        while (true)
        {
            if (killCount > 200)
            {
                SpawnProjectile(.2f);
                SpawnProjectile(-.2f);
            }
            yield return new WaitForSeconds(.5f);
        }
    }


    public void EndAttack()
    {
        //UnlockMovement();
        swordAttack.StopAttack();
    }
}
