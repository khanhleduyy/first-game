using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public enum Modes
    { melee, Follow, Straight, Spread }

    public GameObject projectiles;
    public float projectilesSpeed;
    public float fireRate;
    public float ammo;
    public float currentAmmo;
    public Transform bulletTransform;
    public Modes projectilesMode;

    public float spread;
    public int ammountOfBullet;


    private float timer;
    private Camera mainCam;
    private Vector3 playerPos;
    public Transform Unit;
    public bool canFire;
    private GameObject Player;

    [SerializeField] private AudioSource gunSoundEffect;



    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        currentAmmo = ammo;
        Player = GameObject.FindGameObjectWithTag("Player");
            
    }
    private void Update()
    {
        MouseRotation();
        if (projectilesMode == Modes.Straight)
        {
            StraightShooting();
        }else if( projectilesMode == Modes.Spread)
        {
            SpreadShooting();
        }


    }
    void MouseRotation()
    {
        if(playerPos != null)
        {
            playerPos = Player.transform.position;

            Vector3 rotation = playerPos - transform.position;

            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            if (transform.rotation.z >= Mathf.Abs(90))
            {

                transform.rotation = Quaternion.Euler(0, 180, rotZ + 180);

            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, rotZ + 180);
            }
        }

        


        transform.position = Unit.position;
    }
    void StraightShooting()
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
            if (currentAmmo < 0)
            {
                currentAmmo = ammo;
            }
            gunSoundEffect.Play();



        }
    }
    void SpreadShooting()
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
            for (int i = 0; i < ammountOfBullet; i++)
            {
                GameObject bulletIns = Instantiate(projectiles, bulletTransform.position, bulletTransform.rotation);
                Vector3 direction = playerPos - transform.position;
                Vector2 spreadVector = new Vector2(Random.Range(-spread, spread), Random.Range(-spread, spread));
                bulletIns.GetComponent<Rigidbody2D>().velocity = (new Vector2(direction.x, direction.y).normalized + spreadVector) * projectilesSpeed;

            }

            currentAmmo--;
            if (currentAmmo < 0)
            {
                currentAmmo = ammo;
            }
            gunSoundEffect.Play();



        }
    }
}
