using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class gun : MonoBehaviour
{
    public event EventHandler OnGunShooting;


    [SerializeField] private GameObject projectiles;
    [SerializeField] private Transform bulletTransform;
    [SerializeField] private Transform unit;
    [SerializeField] private AudioClipSO audioClipSO;

    private float projectileSpeed = 30f;
    private float fireRate = .5f;
    private float maxAmmo = 3;
    private float currentAmmo;
    private float spread;
    private float reloadingtime;
    private float rateOfFire = 3f;

    private float timer;
    private Camera mainCam;
    private Vector3 mousePos;
    private bool canFire;


    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        MouseRotation();
        if (!platformGameManager.Instance.IsGamePlaying())
        {
            return;
        }

        if (platformGameManager.Instance.IsGameOver())
        {
            return;
        }
        
        Shooting();
        
    }


    void MouseRotation()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ + 180);

        transform.position = unit.position;
    }
    void Shooting()
    {
        Vector2 spreadVector = new Vector2(UnityEngine.Random.Range(-spread, spread), UnityEngine.Random.Range(-spread, spread));

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > fireRate)
            {
                canFire = true;
                timer = 0;
            }
        }
        if (Input.GetMouseButton(0) && canFire && currentAmmo> 0)
        {
            canFire = false;
            GameObject bulletIns = Instantiate(projectiles, bulletTransform.position, bulletTransform.rotation);
            Vector3 direction = mousePos - transform.position;
            bulletIns.GetComponent<Rigidbody2D>().velocity = (new Vector2(direction.x, direction.y).normalized + spreadVector) * projectileSpeed;
            currentAmmo--;
            OnGunShooting?.Invoke(this, EventArgs.Empty);
            AudioSource.PlayClipAtPoint(audioClipSO.shoot, Camera.main.transform.position, .5f);
            

        }
        if (currentAmmo <= 0)
        {
            reloadingtime += Time.deltaTime;
            if (reloadingtime >= rateOfFire)
            {
                currentAmmo = maxAmmo;
                reloadingtime = 0;
            }

        }
    }

}
