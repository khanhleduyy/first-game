using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnImpact : MonoBehaviour
{
    private const string LAVA = "Lava";

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(LAVA))
        {
            Destroy(gameObject);
        }
    }
}
