using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    internal PlayerMovement movementScripts;

    [SerializeField]
    internal GunChangeScripts gunChangeScripts;


    internal Rigidbody2D playerRb;
    public PlayerEquipment weapon;
    public PlayerEquipment CurrentAmmo;
    public SpriteRenderer playerSprite;
    


    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<PlayerEquipment>();
        CurrentAmmo = GetComponentInChildren<PlayerEquipment>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
