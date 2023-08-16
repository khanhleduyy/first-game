using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChangeScripts : MonoBehaviour
{
    [SerializeField]
    Player playerScripts;

    public bool canReset = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject whatHit = collision.gameObject;
        if (whatHit.CompareTag("WeaponBox"))
        {
            playerScripts.weapon.guns[0].SetActive(false);
            playerScripts.weapon.guns[1].SetActive(false);
            playerScripts.weapon.guns[2].SetActive(false);
            playerScripts.weapon.guns[3].SetActive(false);
            playerScripts.weapon.guns[Random.Range(1, 4)].SetActive(true);

        }
    }
    private void Update()
    {
        
        if (playerScripts.CurrentAmmo.weaponScripts.currentAmmo <= 0 && !canReset)
        {
            playerScripts.weapon.guns[3].SetActive(false);
            playerScripts.weapon.guns[2].SetActive(false);
            playerScripts.weapon.guns[1].SetActive(false);
            playerScripts.weapon.guns[0].SetActive(true);
            canReset = true;
        }
        else if (playerScripts.CurrentAmmo.weaponScripts.currentAmmo > 0)
        {
            canReset = false;
        }


    }
}
