using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField] private AudioSource explosionSoundEffect;

    public GameObject explosionFx;

    public float fieldOfImpact;
    public float force;

    private float damage = 20f;

    public LayerMask LayerToHit;

    

    private void Awake()
    {
        explosionSoundEffect = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, LayerToHit);

        
        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;         
            Debug.Log(obj.gameObject.tag);
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);

            PlayerMovement playerHit = obj.gameObject.GetComponent<PlayerMovement>();
            if(playerHit != null)
            {
                playerHit.Damage(damage);
            }

            Enemy enemyHit = obj.gameObject.GetComponent<Enemy>();
            if(enemyHit != null)
            {
                enemyHit.Damage(damage);
            }
        }             
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Projectiles"))
        {
            Destroy(gameObject);
            Instantiate(explosionFx, transform.position, transform.rotation);
            explosionSoundEffect.Play();
            Explode();
        }
    }
}
