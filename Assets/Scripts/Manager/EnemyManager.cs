using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject[] normalEnemies;
    public GameObject[] bigEnemies;
    public GameObject[] smallEnemies;
    public GameObject[] suicideEnemies;
    public GameObject[] explosiveBarrels;
    public Transform NEnemyContainer;
    public Transform BEnemyContainer;
    public Transform SEnemyContainer;
    public Transform SuiEnemyContainer;
    public Transform ExBarrelContainer;

    public int normalEnemy;
    public int bigEnemy;
    public int smallEnemy;
    public int suicideEnemy;
    public int exBarrel;
    
    
    private bool canSpawnNEnemy;
    private bool canSpawnBEnemy;
    private bool canSpawnSEnemy;
    private bool canSpawnSuiEnemy;
    private bool canSpawnExBarrel;

    private int maxNormalEnemy = 5;
    private int maxBigEnemy = 2;
    private int maxSmallEnemy = 2;
    private int maxSuiEnemy = 3;
    private int maxExBarrel = 2;

    private float nEnemyTimer;
    private float nEnemyMaxTime = 12;
    private float nEnemyMinTime = 5;

    private float bEnemyTimer;
    private float bEnemyMaxTime = 20;
    private float bEnemyMinTime = 7;

    private float sEnemyTimer;
    private float sEnemyMaxTime = 20;
    private float sEnemyMinTime = 7;

    private float suiEnemyTimer;
    private float suiEnemyMaxTime = 30;
    private float suiEnemyMinTime = 10;

    private float exBarrelTimer;
    private float exBarrelMaxTime = 20;
    private float exBarrelMinTime = 10;

    
    
    void Start()
    {
        
    }
    void Update()
    {
        spawnNormalEnemy();
        SpawnBigEnemy();
        SpawnSmallEnemy();
        SpawnSuicideEnemy();
        SpawnExplosiveBarrel();
    }
    public void spawnNormalEnemy()
    {
        nEnemyTimer += Time.deltaTime;

        float timeToSpawn = Random.Range(nEnemyMinTime, nEnemyMaxTime);

        normalEnemy = NEnemyContainer.childCount;

        if(nEnemyTimer >= timeToSpawn && canSpawnNEnemy)
        {
            
            SpawnOneNEnemy(normalEnemies);
            nEnemyTimer = 0;           
            normalEnemy++;
            
                      
        }
        if(normalEnemy >= maxNormalEnemy)
        {
            canSpawnNEnemy = false;
            
        }
        else
        {
            canSpawnNEnemy = true;
        }
              
    }
    private void SpawnOneNEnemy(GameObject[] enemyToSpawn)
    {
        float x = Random.Range(-15, 15);
        float y = MapManager.floorPos.y + 9;
        int rand = Random.Range(0, enemyToSpawn.Length);

        
        Vector2 spawnPos = new Vector2(x, y);
        Instantiate(enemyToSpawn[rand], spawnPos, transform.rotation, NEnemyContainer);
        

        
        
        
    }
    public void SpawnBigEnemy()
    {
        bEnemyTimer += Time.deltaTime;

        float timeToSpawn = Random.Range(bEnemyMinTime, bEnemyMaxTime);

        bigEnemy = BEnemyContainer.childCount;

        if (bEnemyTimer >= timeToSpawn && canSpawnBEnemy)
        {
            SpawnOneBEnemy(bigEnemies);
            bEnemyTimer = 0;
            bigEnemy++;
            if(bEnemyMaxTime > bEnemyMinTime)
            {
                bEnemyMaxTime -= 0.1f;
            }
            


        }
        if (bigEnemy >= maxBigEnemy)
        {
            canSpawnBEnemy = false;

        }
        else
        {
            canSpawnBEnemy = true;
        }
    }

    private void SpawnOneBEnemy(GameObject[] enemyToSpawn)
    {
        float x = Random.Range(-20, 20);
        float y = MapManager.floorPos.y + 6;
        int rand = Random.Range(0, enemyToSpawn.Length);

        Vector2 spawnPos = new Vector2(x, y);

        Instantiate(enemyToSpawn[rand], spawnPos, transform.rotation, BEnemyContainer);
    }

    public void SpawnSmallEnemy()
    {
        sEnemyTimer += Time.deltaTime;

        float timeToSpawn = Random.Range(sEnemyMinTime, sEnemyMaxTime);

        smallEnemy = SEnemyContainer.childCount;

        if (sEnemyTimer >= timeToSpawn && canSpawnSEnemy)
        {
            SpawnOneSEnemy(smallEnemies);
            sEnemyTimer = 0;
            smallEnemy++;
            if (sEnemyMaxTime > sEnemyMinTime)
            {
                sEnemyMaxTime -= 0.1f;
            }



        }
        if (smallEnemy >= maxSmallEnemy)
        {
            canSpawnSEnemy = false;

        }
        else
        {
            canSpawnSEnemy = true;
        }
    }
    private void SpawnOneSEnemy(GameObject[] enemyToSpawn)
    {
        float x = Random.Range(-20, 20);
        float y = MapManager.floorPos.y + 6;
        int rand = Random.Range(0, enemyToSpawn.Length);

        Vector2 spawnPos = new Vector2(x, y);

        Instantiate(enemyToSpawn[rand], spawnPos, transform.rotation, SEnemyContainer);
    }

    public void SpawnSuicideEnemy()
    {
        suiEnemyTimer += Time.deltaTime;

        float timeToSpawn = Random.Range(suiEnemyMinTime, suiEnemyMaxTime);

        suicideEnemy = SuiEnemyContainer.childCount;

        if (suiEnemyTimer >= timeToSpawn && canSpawnSuiEnemy)
        {
            SpawnOneSuiEnemy(suicideEnemies);
            suiEnemyTimer = 0;
            suicideEnemy++;
            if (suiEnemyMaxTime > suiEnemyMinTime)
            {
                suiEnemyMaxTime -= 0.1f;
            }



        }
        if (suicideEnemy >= maxSuiEnemy)
        {
            canSpawnSuiEnemy = false;

        }
        else
        {
            canSpawnSuiEnemy = true;
        }
    }
    private void SpawnOneSuiEnemy(GameObject[] enemyToSpawn)
    {
        float x = Random.Range(-20, 20);
        float y = MapManager.floorPos.y + 6;
        int rand = Random.Range(0, enemyToSpawn.Length);

        Vector2 spawnPos = new Vector2(x, y);

        Instantiate(enemyToSpawn[rand], spawnPos, transform.rotation, SuiEnemyContainer);
    }

    public void SpawnExplosiveBarrel()
    {
        exBarrelTimer += Time.deltaTime;

        float timeToSpawn = Random.Range(exBarrelMinTime, exBarrelMaxTime);

        exBarrel = ExBarrelContainer.childCount;

        if (exBarrelTimer >= timeToSpawn && canSpawnExBarrel)
        {
            SpawnOneExBarrel(explosiveBarrels);
            exBarrelTimer = 0;
            exBarrel++;
          



        }
        if (exBarrel >= maxExBarrel)
        {
            canSpawnExBarrel = false;

        }
        else
        {
            canSpawnExBarrel = true;
        }
    }
    private void SpawnOneExBarrel(GameObject[] barrelToSpawn)
    {
        float x = Random.Range(-20, 20);
        float y = MapManager.floorPos.y + 6;
        int rand = Random.Range(0, barrelToSpawn.Length);

        Vector2 spawnPos = new Vector2(x, y);

        Instantiate(barrelToSpawn[rand], spawnPos, transform.rotation, ExBarrelContainer);
    }
}
