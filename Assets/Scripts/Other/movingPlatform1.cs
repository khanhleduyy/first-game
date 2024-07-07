using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform1 : MonoBehaviour
{
    private const string PLAYER = "Player";
    private const string PLAYER_VFX = "PlayerVFX";
    private const string ENEMY_VFX = "EnemyVFX";
    private const string ENEMY = "Enemy";

    private Vector2 startPos;

	[SerializeField] private float height;
	[SerializeField] private float speed;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(PLAYER);
        enemy = GameObject.FindGameObjectWithTag(ENEMY);
    }

    private void Start()
	{
		startPos = transform.position;

	}

	// Update is called once per frame
	void Update()
	{
		float x = Mathf.PingPong(Time.time * speed, height);
		transform.position = new Vector2(startPos.x + x, startPos.y);
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(PLAYER_VFX))
        {
            if (transform.position.y < collision.transform.position.y)
            {
                player.transform.SetParent(transform);

            }
        }else if (collision.collider.CompareTag(ENEMY_VFX))
        {
            if (transform.position.y < collision.transform.position.y)
            {
                enemy.transform.SetParent(transform);

            }
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(PLAYER_VFX))
        {
            player.transform.SetParent(null);
        }
        else if (collision.collider.CompareTag(ENEMY_VFX))
        {
           
            
            enemy.transform.SetParent(null);

            
        }

    }
}
