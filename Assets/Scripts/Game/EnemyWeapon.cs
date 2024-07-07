using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private const string PLAYERVFX = "PlayerVFX";
  
    [SerializeField] private GameObject projectiles;
    [SerializeField] private Transform bulletTransform;
    [SerializeField] private Transform Unit;
    [SerializeField] private AudioClipSO audioClipSO;
    private GameObject Player;

    [SerializeField] private float projectilesSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private float ammo;
    [SerializeField] private float currentAmmo;
    [SerializeField] private float spread;

    [SerializeField] private int ammountOfBullet;


    private float timer;
    private Vector3 playerPos;
    private bool canFire;




    void Start()
    {
        
        currentAmmo = ammo;
        Player = GameObject.FindGameObjectWithTag(PLAYERVFX);
            
    }
    private void Update()
    {
        if (!platformGameManager.Instance.IsGamePlaying())
        {
            return;
        }

        if (platformGameManager.Instance.IsGameOver())
        {
            return;
        }
        EnemyPos();
        EnemyShooting();


    }
    void EnemyPos()
    {
        if(playerPos != null)
        {
            playerPos = Player.transform.position;

            Vector3 rotation = playerPos - transform.position;

            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ + 180 );
        }

        


        transform.position = Unit.position;
    }
    void EnemyShooting()
    {
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > fireRate)
            {
                canFire = true;
                timer = 0;
            }
        }
        if (canFire)
        {
            canFire = false;
            GameObject bulletIns = Instantiate(projectiles, bulletTransform.position, bulletTransform.rotation);
            Vector3 direction = playerPos - transform.position;
            Vector2 spreadVector = new Vector2(Random.Range(-spread, spread), Random.Range(-spread, spread));
            bulletIns.GetComponent<Rigidbody2D>().velocity = (new Vector2(direction.x, direction.y).normalized + spreadVector) * projectilesSpeed;
            currentAmmo--;
            AudioSource.PlayClipAtPoint(audioClipSO.shoot, Camera.main.transform.position, .1f);
            if (currentAmmo < 0)
            {
                currentAmmo = ammo;
            }
        }
    }
    
}
