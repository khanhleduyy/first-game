using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject chestOpenFx;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(chestOpenFx, transform.position, transform.rotation);
            Destroy(gameObject);
        }else if (other.gameObject.CompareTag("Lava"))
        {
            Destroy(gameObject);
        }
    }
}
