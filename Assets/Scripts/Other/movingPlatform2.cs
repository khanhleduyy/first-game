using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform2 : MonoBehaviour
{
    private const string PLAYER = "Player";
    private const string PLAYER_VFX = "PlayerVFX";
    private const string ENEMY_VFX = "EnemyVFX";
    private const string ENEMY = "Enemy";



    private Vector2 startPos;

    [SerializeField] private float height;
    [SerializeField] private float speed;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] enemyArray;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(PLAYER);
        enemyArray = GameObject.FindGameObjectsWithTag(ENEMY);

    }

    private void Start()
    {
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, height);
        transform.position = new Vector2(startPos.x , startPos.y + y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(PLAYER_VFX))
        {
            if (transform.position.y < collision.transform.position.y)
            {
                player.transform.SetParent(transform);

            }
        }
        else if (collision.collider.CompareTag(ENEMY_VFX))
        {
            if (transform.position.y < collision.transform.position.y)
            {
                foreach (GameObject enemy in enemyArray)
                {
                    enemy.transform.SetParent(transform);
                }

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
            foreach (GameObject enemy in enemyArray)
            {
                enemy.transform.SetParent(null);
            }
        }

    }
}
