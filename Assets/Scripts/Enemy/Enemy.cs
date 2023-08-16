using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioSource explosionSoundEffect;
    public enum Type
    { gun, suicide}

    public Type typeOfEnemy;
    private GameObject Player;
    public Rigidbody2D EnemyRb;
    public float speed;
    public float distance;
    public float checkRadius;
    public float health;
    public LayerMask whatIsGrounded;
    public Transform groundDetection;
    public Transform groundCheck;
    public bool canJump;
    public int extraJumpValue;
    public int extraJump;
    public int scorePoint;


    private float timer;
    private float jumpingForce = 20f;
    private bool isGrounded;

    private SpriteRenderer enemySprite;
    private float frameFlashing = 7f;
    protected Color enemyColor;

    private float damage = 5f;

    public GameObject suicideExplosionFx;
    public GameObject DestroyFx;
    private void Awake()
    {
        explosionSoundEffect = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        EnemyRb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        enemySprite = GetComponent<SpriteRenderer>();
        enemyColor = enemySprite.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyBehaviour();
        EnemyMovement();
        
    }
    void EnemyBehaviour()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            Jump();
        }
    }
    void EnemyMovement()
    {
        if (transform.position.x < Player.transform.position.x )
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }else if (transform.position.x > Player.transform.position.x)
        {
            transform.position += new Vector3(-1 , 0, 0) * Time.deltaTime * speed;
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }
    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGrounded);
        if (!canJump)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                canJump = true;
                timer = 0;
            }
        }
        if (isGrounded)
        {
            extraJump = extraJumpValue;
        }
        if ( extraJump > 0 && canJump)
        {
            EnemyRb.velocity = Vector2.up * jumpingForce;
            extraJump--;
            canJump = false;
            /*squashStetchAnimator.SetTrigger("Jump");
            Instantiate(jumpFx, transform.position, transform.rotation);
            jumpSoundEffect.Play();*/
        }
        else if (extraJump == 0 && isGrounded && canJump)
        {
            EnemyRb.velocity = Vector2.up * jumpingForce;
            canJump = false;
            //squashStetchAnimator.SetTrigger("Jump");
            //Instantiate(jumpFx, transform.position, transform.rotation);
            //jumpSoundEffect.Play();
        }
    }

   

     
    public void Damage(float damage)
    {
        health -= damage;
        DamageFlash();
        if(health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
        Instantiate(DestroyFx, transform.position, transform.rotation);
        ScoreManager.scoreValue += scorePoint;
        
    }

    private void DamageFlash()
    {
        enemySprite.color = new Color(1, 1, 1, 1f);
        Invoke("ResetColor", Time.deltaTime * frameFlashing);
    }
    private void ResetColor()
    {
        enemySprite.color = enemyColor;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            Kill();
        }else if(other.gameObject.CompareTag("Player") && typeOfEnemy == Type.suicide)
        {
            PlayerMovement playerHit = other.gameObject.GetComponent<PlayerMovement>();

            if (playerHit != null)
            {
                Vector3 distance = playerHit.transform.position - transform.position;
                playerHit.Damage(damage);
                playerHit.playerRb.AddForce(distance * 1000f);
                explosionSoundEffect.Play();
                Instantiate(suicideExplosionFx, transform.position, transform.rotation);
                Kill();
            }
        }else if (other.gameObject.CompareTag("WeaponBox") && typeOfEnemy == Type.suicide)
        {
            Rigidbody2D weaponBoxRb = other.gameObject.GetComponent<Rigidbody2D>();
            GameObject weaponPos = other.gameObject;
            Vector3 distance = weaponPos.transform.position - transform.position;
            weaponBoxRb.AddForce(distance * 1000f);
            explosionSoundEffect.Play();
            Instantiate(suicideExplosionFx, transform.position, transform.rotation);
            Kill();
        }
    }
    
}
