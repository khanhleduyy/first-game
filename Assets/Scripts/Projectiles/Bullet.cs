using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage = 20f; 
    private float timeToDestroy = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, timeToDestroy);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyHit = other.gameObject.GetComponent<Enemy>();
            

            if(enemyHit != null)
            {
                Vector3 distance = enemyHit.transform.position - transform.position ;
                enemyHit.Damage(damage);
                enemyHit.EnemyRb.AddForce(distance * 500f);
                Destroy(gameObject);
                
            }
        }
        
    }
}
