using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAi : MonoBehaviour
{


    [Header("Assigned")]
    public GameObject enemyVisual;
    public Rigidbody2D enemyRb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGrounded;
    [SerializeField] private LayerMask lava;
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private GameObject destroyFx;
    [SerializeField] private AudioClipSO audioClipSO;
    private Path path;
    private Seeker seeker;
    private const string PLAYER_VFX = "PlayerVFX";
    private SpriteRenderer enemySprite;

    [Header("Pathfinding")]
    [SerializeField] private GameObject target;
    private float activateDistance = 200f;
    private float pathUpdateSeconds = .5f;
    private float nextWayPointDistance = 3f;
    private int currentWayPoint = 0;

    [Header("Physics")]
    [SerializeField] private float jumpingForce = 20f;
    private float jumpNodeHeightRequirement = .8f;
    private float checkRadius = .6f;
    private float lavaCheckRadius = 1f;
    private float timer;
    private float fallMultiplier = 3f;
    private float lowJumpMultiplier = 2f;
    private float velocityOffset = .05f;
    private float enemyMaxSpeed = 5f;
    private bool followEnabled = true;
    private bool canJump = false;
    private bool isFacingRight = true;

    [Header("enemyData")]
    [SerializeField] private float health;
    [SerializeField] private float speed;
    private float frameFlashing = 7f;
    private Color enemyColor;
    [SerializeField] private int scorePoint;


    

    private void Start()
    {
        
        seeker = enemyVisual.GetComponent<Seeker>();
        enemyRb = enemyVisual.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag(PLAYER_VFX);
        enemySprite = enemyVisual.GetComponent<SpriteRenderer>();
        enemyColor = enemySprite.color;
        
        health = enemySO.maxHealthAmmount;
        speed = enemySO.maxSpeedAmmount;

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    
    private void FixedUpdate()
    {
        if (!platformGameManager.Instance.IsGamePlaying())
        {
            DestroyGameObject();
            return;
        }
        if (platformGameManager.Instance.IsGameOver())
        {
            DestroyGameObject();
            return;
        }
        if (TargetInDistance() && followEnabled)
        {
        PathFollow();

        }
        DestroyGameObjectOnContact();

    }

    private void Update()
    {
        //Debug.Log(health);
        
    }


    private void UpdatePath()
    {
        
        if(followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(enemyRb.position, target.transform.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if(path == null)
        {
            return;
        }

        if(currentWayPoint >= path.vectorPath.Count)
        {
            return;
        }

        //direction
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - enemyRb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;


        //Jump
        
        if (!canJump)
        {
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                canJump = true;
                timer = 0;
            }
        }
        if (canJump && IsGrounded())
        {
            if(direction.y > jumpNodeHeightRequirement)
            {
                enemyRb.velocity = Vector2.up * jumpingForce;
                canJump = false;
                AudioSource.PlayClipAtPoint(audioClipSO.jump, Camera.main.transform.position, .1f);
            }
        }
        if (enemyRb.velocity.y < -velocityOffset)
        {
            enemyRb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else if (enemyRb.velocity.y > velocityOffset)
        {
            enemyRb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }

        //Movement
        if(enemyRb.velocity.x < enemyMaxSpeed && enemyRb.velocity.x > -enemyMaxSpeed)
        {
            enemyRb.AddForce(force, ForceMode2D.Impulse);
        }

        //NextWayPoint
        float distance = Vector2.Distance(enemyRb.position, path.vectorPath[currentWayPoint]);
        if(distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }

        if (isFacingRight && enemyRb.velocity.x < -velocityOffset || !isFacingRight && enemyRb.velocity.x > velocityOffset)
        {
            Vector3 localScale = enemyVisual.transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            enemyVisual.transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {

        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGrounded);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 groundcheck = new Vector3(groundCheck.position.x, groundCheck.position.y);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundcheck, checkRadius);
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(enemyVisual.transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        DamageFlash();
        Debug.Log(damage);
        if (health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Instantiate(destroyFx, enemyVisual.transform.position, enemyVisual.transform.rotation);
        ScoreManager.scoreValue += scorePoint;
        Destroy(gameObject);

    }
    private void DestroyGameObject()
    {
        Destroy(gameObject);
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

    private void DestroyGameObjectOnContact()
    {
        if (IsLava())
        {
            Kill();
        }
    }

    private bool IsLava()
    {

        return Physics2D.OverlapCircle(groundCheck.position, lavaCheckRadius, lava);
    }

}
