using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
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
        
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerHit = other.gameObject.GetComponent<PlayerMovement>();

            if (playerHit != null)
            {
                Vector3 distance = playerHit.transform.position - transform.position;
                playerHit.Damage(damage);
                playerHit.playerRb.AddForce(distance * 500f);
                Destroy(gameObject);
            }
        }
    }
}
