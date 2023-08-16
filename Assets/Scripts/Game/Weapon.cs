using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Modes
    { melee, Follow, Straight, Spread}

    public GameObject projectiles;
    public float projectilesSpeed;
    public float fireRate;
    public float ammo;
    public float currentAmmo;
    public float spread;
    public int ammountOfBullet;
    public Transform bulletTransform;
    public Modes projectilesMode;


    private float timer;
    private Camera mainCam;
    private Vector3 mousePos;
    public Transform Unit;
    public bool canFire;

    private Transform target;
    [SerializeField] private AudioSource gunSoundEffect;



    void Start()
    {
        
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        currentAmmo = ammo;
        //target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
    }
    private void Update()
    {
        MouseRotation();
        if (projectilesMode == Modes.Straight)
        {
            StraightShooting();
        }else if (projectilesMode == Modes.Spread)
        {
            SpreadShooting();
        }else if(projectilesMode == Modes.Follow)
        {
            FollowShooting();
        }
        
        
    }
    void MouseRotation()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg ;
        transform.rotation = Quaternion.Euler(0, 0, rotZ + 180);
        
        

        transform.position = Unit.position;
    }
    void StraightShooting()
    {
        Vector2 spreadVector = new Vector2(Random.Range(-spread, spread), Random.Range(-spread, spread));

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > fireRate)
            {
                canFire = true;
                timer = 0;
            }
        }
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            GameObject bulletIns =  Instantiate(projectiles, bulletTransform.position, bulletTransform.rotation);
            Vector3 direction = mousePos - transform.position;
            bulletIns.GetComponent<Rigidbody2D>().velocity = (new Vector2(direction.x, direction.y).normalized + spreadVector) * projectilesSpeed ;
            currentAmmo--;
            if (currentAmmo <0)
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
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            for (int i = 0; i < ammountOfBullet; i++)
            {
                GameObject bulletIns = Instantiate(projectiles, bulletTransform.position, bulletTransform.rotation);
                Vector3 direction = mousePos - transform.position;
                Vector2 spreadVector = new Vector2(Random.Range(-spread, spread), Random.Range(-spread, spread));
                bulletIns.GetComponent<Rigidbody2D>().velocity = (new Vector2(direction.x, direction.y).normalized + spreadVector) * projectilesSpeed ;
                
            }
            
            currentAmmo--;
            if (currentAmmo < 0)
            {
                currentAmmo = ammo; 
            }
            gunSoundEffect.Play();



        }
    }

    void FollowShooting()
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
        if (Input.GetMouseButton(0) && canFire) //&& target!=null)
        {
            canFire = false;
            GameObject bulletIns = Instantiate(projectiles, bulletTransform.position, bulletTransform.rotation);
            Vector3 direction = mousePos - transform.position;
            bulletIns.GetComponent<Rigidbody2D>().velocity = (new Vector2(direction.x, direction.y).normalized ) * projectilesSpeed;
            currentAmmo--;
            if (currentAmmo < 0)
            {
                currentAmmo = ammo;
            }
            gunSoundEffect.Play();
        }
    }


}
