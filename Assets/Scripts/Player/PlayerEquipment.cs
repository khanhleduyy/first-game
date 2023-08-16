using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{

    

    int weaponTotal = 1;
    public static int currentWeaponIndex;

    public GameObject[] guns;
    public GameObject weaponHolder;
    public GameObject currentGun;
    public Weapon weaponScripts;
    void Start()
    {
        

        weaponTotal = weaponHolder.transform.childCount;
        guns = new GameObject[weaponTotal];

        for (int i = 0; i < weaponTotal; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }

        guns[0].SetActive(true);
        currentGun = guns[0];
        currentWeaponIndex = 0;
        
        
        weaponScripts = transform.GetChild(1).GetComponent<Weapon>();
        weaponScripts = transform.GetChild(2).GetComponent<Weapon>();
        weaponScripts = transform.GetChild(3).GetComponent<Weapon>();
        
        
    }
    private void Update()
    {

        
        
    }
    
    
}
