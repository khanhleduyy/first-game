using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    private const string PLAYER_VFX = "PlayerVFX";

    [SerializeField] private GameObject playerVisual;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;
    void Start()
    {
        playerVisual = GameObject.FindGameObjectWithTag(PLAYER_VFX);
    }

    
    void Update()
    {
        if(playerVisual == null)
        {
            playerVisual = GameObject.FindGameObjectWithTag(PLAYER_VFX);
        }
        if (playerVisual != null)
        {
            Vector3 desiredPos = playerVisual.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPos, speed * Time.deltaTime);
        }
        
    }
}
