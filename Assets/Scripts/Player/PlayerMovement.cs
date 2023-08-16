using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Player playerScripts;

    [SerializeField] private AudioSource jumpSoundEffect;

    public GameObject jumpFx;

    public float speed = 5f;
    public Transform groundCheck;
    public float checkRadius;
    public int extraJumpValue;
    public LayerMask whatIsGrounded;
    public Animator squashStetchAnimator;
    public int extraJump;
    public bool canJump;
    public Rigidbody2D playerRb;



    private float jumpingForce = 20f;
    private float timer;
    private bool isGrounded;

    
    private float frameFlashing = 7f;
    protected Color playerColor;

    //health
    public float health;
    public float maxHealth = 1000f;
    public HealthBar healthBar;

    public GameObject score;
    public GameObject highScore;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highScoreText;

    private Shake shake;
    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerColor = playerScripts.playerSprite.color;
        squashStetchAnimator = GetComponent<Animator>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        scoreText = score.GetComponent<TextMeshProUGUI>();
        highScoreText = highScore.GetComponent<TextMeshProUGUI>();
        PlayerPrefs.SetString("currentScore", "0");
        PlayerPrefs.SetString("highScore", "0");
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

    void Update()
    {
        MoveMent();
        Jump();
        
    }
    void MoveMent()
    {
        

        var movement = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * speed;

        
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
        if (Input.GetKeyDown(KeyCode.W) && extraJump > 0 && canJump)
        {
            playerScripts.playerRb.velocity = Vector2.up * jumpingForce;
            extraJump--;
            canJump = false;
            squashStetchAnimator.SetTrigger("Jump");
            Instantiate(jumpFx, transform.position, transform.rotation);
            jumpSoundEffect.Play();

           

            
        }
        else if (Input.GetKeyDown(KeyCode.W) && extraJump == 0 && isGrounded && canJump)
        {
            playerScripts.playerRb.velocity = Vector2.up * jumpingForce;
            canJump = false;
            squashStetchAnimator.SetTrigger("Jump");
            Instantiate(jumpFx, transform.position, transform.rotation);
            jumpSoundEffect.Play();

            
           
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        DamageFlash();
        shake.CamShake();
        if (health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        PlayerPrefs.SetString("currentScore", scoreText.text);
        PlayerPrefs.SetString("highScore", highScoreText.text);
        Destroy(gameObject);
        ChangeScene();
      
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene(2);
    }

    private void DamageFlash()
    {
        playerScripts.playerSprite.color = new Color(1, 1, 1, 1f);
        Invoke("ResetColor", Time.deltaTime * frameFlashing);
    }
    private void ResetColor()
    {
        playerScripts.playerSprite.color = playerColor;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            health = 0;
            Kill();
        }
    }
}
