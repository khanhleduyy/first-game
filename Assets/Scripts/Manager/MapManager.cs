using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static Vector2 rightPos, leftPos;
    public static Vector2 middlePos;
    public static Vector2 floorPos;

    public GameObject[] sidePlatform;
    public GameObject[] middlePlatform;
    public GameObject[] floor;

    public Transform MapContainer;

    private List<GameObject> parts;
    // Start is called before the first frame update
    void Start()
    {
        parts = new List<GameObject>();
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void GenerateMap()
    {
        parts = new List<GameObject>();
        rightPos = new Vector2(-27, 5);
        leftPos = new Vector2(27, 5);
        floorPos = new Vector2(0, -7);
        //middlePos = new Vector2(0, 7);

        parts.Add(Instantiate(floor[Random.Range(0, sidePlatform.Length)], floorPos, transform.rotation, MapContainer));
        //parts.Add(Instantiate(middlePlatform[Random.Range(0, sidePlatform.Length)], middlePos, transform.rotation, MapContainer));
        parts.Add(Instantiate(sidePlatform[Random.Range(0, sidePlatform.Length)], rightPos, transform.rotation, MapContainer));
        parts.Add(Instantiate(sidePlatform[Random.Range(0, sidePlatform.Length)], leftPos, transform.rotation, MapContainer));
    }

    public void CleanMap()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            Destroy(parts[i]);
        }
    }
}
