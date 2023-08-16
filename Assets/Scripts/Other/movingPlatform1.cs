using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform1 : MonoBehaviour
{
	private Vector2 startPos;

	public float height;
	public float speed;

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
        if (collision.collider.CompareTag("Player"))
        {
            if (transform.position.y < collision.transform.position.y)
            {
                collision.transform.SetParent(transform);

            }
        }else if (collision.collider.CompareTag("Enemy"))
        {
            if (transform.position.y < collision.transform.position.y)
            {
                collision.transform.SetParent(transform);

            }
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
        else if (collision.collider.CompareTag("Enemy"))
        {
           
            
            collision.transform.SetParent(null);

            
        }

    }
}
