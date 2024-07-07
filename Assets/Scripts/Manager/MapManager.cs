using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject[] upperPlatform;
    [SerializeField] private GameObject[] lowerPlatform;
    [SerializeField] private GameObject aStar;
    [SerializeField] private Transform MapContainer;
    private AstarPath astarPath;

    private Vector2 lowerPos;
    public Vector2 upperPos;
    private float platformPos = 6f;

 

    private List<GameObject> parts;
    // Start is called before the first frame update

    private void Awake()
    {
        astarPath = aStar.GetComponent<AstarPath>();
    }

    void Start()
    {
        parts = new List<GameObject>();
        GenerateMap();

        InvokeRepeating("UpdateScan", 0f, .2f);
        GameOverUI.Instance.OnRestartButtonClicked += GameOverUI_OnRestartButtonClicked;
    }

    private void GameOverUI_OnRestartButtonClicked(object sender, EventArgs e)
    {
        CleanMap();
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void UpdateScan()
    {
        astarPath.Scan();
    }

    public void GenerateMap()
    {
        parts = new List<GameObject>();
        lowerPos = new Vector2(0, -platformPos);
        upperPos = new Vector2(0, platformPos);

        parts.Add(Instantiate(lowerPlatform[UnityEngine.Random.Range(0, lowerPlatform.Length)], lowerPos, transform.rotation, MapContainer));
        //parts.Add(Instantiate(middlePlatform[Random.Range(0, sidePlatform.Length)], middlePos, transform.rotation, MapContainer));
        parts.Add(Instantiate(upperPlatform[UnityEngine.Random.Range(0, upperPlatform.Length)], upperPos, transform.rotation, MapContainer));
    }

    public void CleanMap()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            Destroy(parts[i]);
        }
    }
}
