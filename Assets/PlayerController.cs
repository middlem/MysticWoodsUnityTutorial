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
    public int maxHp = 100;
    public int currentHp = 100;

    public int playerLevel = 1;
    public int currentXp = 0;
    public int nextLevelXp = 100;
    public AudioClip levelUpClip;

    public bool bossSpawned = false;
    public enum Player_Direction { Left, Right }
    public Player_Direction playerDirection;

    public AudioSource playerAudioSource;

    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] public GameObject levelUpPanel;
    [SerializeField] HealthBar healthBar;
    [SerializeField] ExperienceBar xpBar;

    Vector2 movementInput;
    Rigidbody2D rb;

    Animator animator;
    SpriteRenderer spriteRenderer;
    MovementDriver movementDriver;
    public KillCount killCount;
    public LevelBanner levelBanner;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAudioSource = GetComponent<AudioSource>();

        playerDirection =  Player_Direction.Right;

        movementDriver = new MovementDriver(rb);
        movementDriver.MoveSpeed = moveSpeed;
        movementDriver.CollisionOffset = collisionOffset;

        gameOverPanel.SetActive(false);
        levelUpPanel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        currentHp -= damage;
        healthBar.SetState(currentHp, maxHp);
        //Debug.Log("Current hp: " + currentHp);

        if (currentHp <= 0)
        {
            Time.timeScale = 0; // Pause Game
            gameOverPanel.SetActive(true);
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
            playerDirection = Player_Direction.Left;
        }
        else if (movementInput.x > 0)
        {
            playerDirection = Player_Direction.Right;
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

    private void PlayerLevelUp()
    {
        Time.timeScale = 0; // Pause Game
        playerAudioSource.clip = levelUpClip;
        playerAudioSource.Play();
        levelBanner.UpdatePlayerLevelText(++playerLevel);
        nextLevelXp = (int)(nextLevelXp * 1.2);
        currentXp = 0;
        levelUpPanel.SetActive(true);
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
                PlayerLevelUp();
            }
            xpBar.UpdateExperienceBar(currentXp, nextLevelXp);
            drop.RemoveDrop();
        }
        else if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            takeDamage(enemy.damage);
        }
    }

    public IEnumerator SpawnEnemy()
    {   while (true)
        {
            Vector3 spawnPos = new Vector3(this.gameObject.transform.position.x + (Random.Range(-1.5f, 1.5f)),
                                           this.gameObject.transform.position.y + (Random.Range(-1.5f, 1.5f)),
                                           this.gameObject.transform.position.z);
            
            Instantiate(enemy, spawnPos, Quaternion.identity);

            if (killCount.killCount > 200 && bossSpawned == false)
            {
                bossSpawned = true;
                SpawnBoss();
            }

            yield return new WaitForSeconds(killCount.killCount < 50 ? 1.0f : .25f);
        }
    }

    public void SpawnBoss()
    {
        Vector3 spawnPos = new Vector3(this.gameObject.transform.position.x + (Random.Range(-3f, 3f)),
                                           this.gameObject.transform.position.y + (Random.Range(-3f, 3f)),
                                           this.gameObject.transform.position.z);

        Instantiate(bossObject, spawnPos, Quaternion.identity);
    }

    public void EndAttack()
    {
        //UnlockMovement();
        swordAttack.StopAttack();
    }
}
