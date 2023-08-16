using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HolmingMissle : MonoBehaviour
{
    [SerializeField] private AudioSource explosionSoundEffect;


    public GameObject rocketFx;
    public GameObject explosionFx;

    private float damage = 50;
    public float speed = 5f;

    public float rotateSpeed = 200f;

    private Rigidbody2D rb;
    private Transform target;
    private float timeToDestroy = 20f;

    public float fieldOfImpact;
    public float force;

    public LayerMask LayerToHit;

    public float timer;

    private void Awake()
    {
        explosionSoundEffect = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        FollowTarget();
        

        
    }

    void FollowTarget()
    {
        //Instantiate(rocketFx, transform.position, transform.rotation);
        timer += Time.deltaTime;
        if (timer >= timeToDestroy)
        {
            Destroy(gameObject);
            Instantiate(explosionFx, transform.position, transform.rotation);
            explosionSoundEffect.Play();
            Explode();
        }
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
            
            
        }
        else if(target != null){
            Vector2 direction = (Vector2)target.position - rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;

            rb.velocity = transform.up * speed;
        }
    }

    void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, LayerToHit);

        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;

            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
            Instantiate(explosionFx, transform.position, transform.rotation);
            explosionSoundEffect.Play();
        } else if (other.gameObject.CompareTag("Enemy"))
        {
            Explode();
            Enemy enemyHit = other.gameObject.GetComponent<Enemy>();
            if (enemyHit != null)
            {

                enemyHit.Damage(damage);
                Destroy(gameObject);
                Instantiate(explosionFx, transform.position, transform.rotation);
                explosionSoundEffect.Play();
            } 


        }
    }
    
}
