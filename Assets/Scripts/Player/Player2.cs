using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Player2 : MonoBehaviour
{
    private const string JUMP_ANIMATION = "Jump";
    private const string GAME_INPUT = "GameInput";
    private const string SCREEN_SHAKE = "ScreenShake";

    public static Player2 Instance { get; private set; }
    public GameObject playerVisual;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGrounded;
    [SerializeField] private LayerMask lava;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private GameObject jumpEffect;
    [SerializeField] private AudioClipSO audioClipSO;
    [SerializeField] private HealthBar healthBar;


    private Animator squashStetchAnimator;
    public Rigidbody2D playerRb;
    private SpriteRenderer playerSprite;
    private Shake shake;

    //Movement
    private bool isFacingRight;      
    private float speed = 7;
    private float timer;

    //Dash
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 20f;
    private float dashingTime = .2f;
    private float dashingCooldown = 2f;

    //Jump
    private float checkRadius = .6f;
    private bool canJump;
    private int extraJump;
    private int extraJumpMaxValue = 1;
    private float jumpingForce = 20f;
    private float coyoteTime = .2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = .2f;
    private float jumpBufferCounter;
    private float fallMultiplier = 3f;
    private float lowJumpMultiplier = 2f;

    //Particle
    [SerializeField] private GameObject destroyFx;

    //PlayerData 
    public float maxHealth = 100f;
    public float health;
    private float frameFlashing = 7f;
    private Color playerColor;
    private bool death = false;

    //score
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject highScore;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highScoreText;
    private void Awake()
    {
        Instance = this;
        playerRb = playerVisual.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        health = maxHealth;
        gameInput = GameObject.FindGameObjectWithTag(GAME_INPUT).GetComponent<GameInput>();
        gameInput.OnDashingAction += GameInput_OnDashingAction;
        squashStetchAnimator = playerVisual.GetComponent<Animator>();
        shake = GameObject.FindGameObjectWithTag(SCREEN_SHAKE).GetComponent<Shake>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        score = GameObject.FindGameObjectWithTag("Score");
        highScore = GameObject.FindGameObjectWithTag("HighScore");
        scoreText = score.GetComponent<TextMeshProUGUI>();
        highScoreText = highScore.GetComponent<TextMeshProUGUI>();
        PlayerPrefs.SetString("currentScore", "0");
        PlayerPrefs.SetString("highScore", "0");
        playerSprite = playerVisual.GetComponent<SpriteRenderer>();
        playerColor = playerSprite.color;
    }

    private void GameInput_OnDashingAction(object sender, System.EventArgs e)
    {
        if (canDash)
        {
            StartCoroutine(Dash());
        }
        
    }


    private void FixedUpdate()
    {
        
        if (!platformGameManager.Instance.IsGamePlaying())
        {
            return;
        }

        if (platformGameManager.Instance.IsGameOver())
        {
            return;
        }
        PlayerMovement();
        DestroyGameObjectOnContact();
        UpdateHealth();
    }

    private void PlayerMovement()
    {

        #region dash
        if (isDashing)
        {
            return;
        }
        #endregion

        #region Movement
        Vector3 playerDir = new Vector3(InputVector().x, 0f, 0f);

        float distance = speed * Time.deltaTime;
        transform.position += playerDir * distance;

        if(isFacingRight && InputVector().x < 0 || !isFacingRight && InputVector().x > 0)
        {
            Vector3 localScale = playerVisual.transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            playerVisual.transform.localScale = localScale;
        }

        
        
        #endregion

        #region jump

        // coyoteTime
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // jumpBuffer
        if(InputVector().y > 0)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // logicJump
        if (!canJump)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                canJump = true;
                timer = 0;
            }
        }
        if (jumpBufferCounter > 0 && extraJump > 0 && canJump && coyoteTimeCounter > 0f)
        {
            playerRb.velocity = Vector2.up * jumpingForce;
            squashStetchAnimator.SetTrigger(JUMP_ANIMATION);
            Instantiate(jumpEffect, playerVisual.transform.position, playerVisual.transform.rotation);
            jumpBufferCounter = 0f;
            canJump = false;
            extraJump--;
            AudioSource.PlayClipAtPoint(audioClipSO.jump, Camera.main.transform.position, .3f);
            if (extraJump == 0)
            {
                return;
            }

        }
        if(InputVector().y > 0 && playerRb.velocity.y > 0)
        {
            coyoteTimeCounter = 0f;
        }

        if(playerRb.velocity.y < 0)
        {
            playerRb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else if (playerRb.velocity.y > 0 && InputVector().y <=0)
        {
            playerRb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }

        if (IsGrounded())
        {
            extraJump = extraJumpMaxValue;
        }
        #endregion


        
        
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = playerRb.gravityScale;
        playerRb.gravityScale = 0f;
        playerRb.velocity = new Vector2(playerRb.transform.localScale.x * -dashingPower, 0f);        
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        playerRb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }



    private Vector2 InputVector()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        return inputVector;
    }

    private bool IsGrounded()
    {

        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGrounded);
         
    }
    public void Damage(float damage)
    {
        health -= damage;
        //healthBar.SetHealth(health);
        DamageFlash();
        shake.CamShake();
        if (health <= 0)
        {
            Kill();
        }
    }

    private void UpdateHealth()
    {
        healthBar.SetHealth(health);
    }

    private void Kill()
    {
        death = true;
        Instantiate(destroyFx, playerVisual.transform.position, playerVisual.transform.rotation);
        PlayerPrefs.SetString("currentScore", scoreText.text);
        PlayerPrefs.SetString("highScore", highScoreText.text);
        Destroy(gameObject);

    }

    private void DamageFlash()
    {
        playerSprite.color = new Color(1, 1, 1, 1f);
        Invoke("ResetColor", Time.deltaTime * frameFlashing);
    }
    private void ResetColor()
    {
        playerSprite.color = playerColor;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 groundcheck = new Vector3(groundCheck.position.x, groundCheck.position.y);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundcheck, checkRadius);
    }

    private void DestroyGameObjectOnContact()
    {
        if (IsLava())
        {
            
            Kill();
        }
    }

    private bool IsLava()
    {

        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, lava);
    }

    public bool IsDeath()
    {
        return death;
    }
    
}
