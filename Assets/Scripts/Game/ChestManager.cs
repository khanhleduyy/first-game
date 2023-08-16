using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public GameObject[] weaponChest;
    public Transform chestContainer;

    public int chest;
    public bool canSpawnChest;
    public float maxChestTime = 12;
    public float minChestTime = 6;

    private int maxChest = 3;
    private float chestTimer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnChest();
    }
    public void SpawnChest()
    {
        chestTimer += Time.deltaTime;

        float timeToSpawn = Random.Range(minChestTime, maxChestTime);

        chest = chestContainer.childCount;

        if (chestTimer >= timeToSpawn && canSpawnChest)
        {
            SpawnOneChest(weaponChest);
            chestTimer = 0;
            chest++;
        }

        if (chest >= maxChest)
        {
            canSpawnChest = false;
        }
        else
        {
            canSpawnChest = true;
        }
    }
    private void SpawnOneChest(GameObject[] chestToSpawn)
    {
        float x = Random.Range(-20, 20);
        float y = Random.Range(MapManager.floorPos.y + 2, MapManager.floorPos.y + 6);
        int rand = Random.Range(0, chestToSpawn.Length);

        Vector2 spawnPos = new Vector2(x, y);

        Instantiate(chestToSpawn[rand], spawnPos, transform.rotation, chestContainer);
    }
}
