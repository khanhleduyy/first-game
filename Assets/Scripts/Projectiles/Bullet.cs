using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const string ENEMY_VFX = "EnemyVFX";
    private const string ENEMY = "Enemy";
    private const string PLAYER_VFX = "PlayerVFX";
    private const string PLAYER = "Player";

    private float damage = 20f;
    private GameObject enemyAi;
    private EnemyAi enemyHit;
    private GameObject player;
    private Player2 playerHit;


    private void Start()
    {
        enemyAi = GameObject.FindGameObjectWithTag(ENEMY);
        if(enemyAi != null)
        {
            enemyHit = enemyAi.GetComponent<EnemyAi>();
        }
        player = GameObject.FindGameObjectWithTag(PLAYER);
        if(player != null)
        {
            playerHit = player.GetComponent<Player2>();
        }
        
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(ENEMY_VFX))
        {
            Vector3 distance = enemyHit.enemyVisual.transform.position - transform.position;
            enemyHit.Damage(damage);
            enemyHit.enemyRb.AddForce(distance * 500f);
            Destroy(gameObject);
   
        }
        else if (other.gameObject.CompareTag(PLAYER_VFX))
        {
            Vector3 distance = playerHit.playerVisual.transform.position - transform.position;
            playerHit.Damage(damage);
            playerHit.playerRb.AddForce(distance * 500f);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
 
}
