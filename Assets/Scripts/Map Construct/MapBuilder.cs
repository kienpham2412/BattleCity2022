using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    public List<GameObject> obstacles = new List<GameObject>();
    public GameObject wall;
    public GameObject mapObject;
    private PathFinder pathFinder;
    public Map map;
    public int mapSize;
    public float wallOffset;

    // Start is called before the first frame update
    void Start()
    {
        pathFinder = GetComponent<PathFinder>();
        map = new Map();
        mapSize = map.mapSize;
        CreateBlankMap();
        // Debug.Log("obstacle size: " + obstacles[1].GetComponent<SpriteRenderer>().bounds.size.x);
    }

    /// <summary>
    /// Construct a blank map
    /// </summary>
    public void CreateBlankMap()
    {
        map.CreateBlank();
        pathFinder.GetPath(map);
        DestroyObstacles();
        BuildBlocks();
    }

    /// <summary>
    /// Construct a random generated map
    /// </summary>
    public void GenerateRandomMap()
    {
        map.Random();
        pathFinder.GetPath(map);
        DestroyObstacles();
        BuildBlocks();
    }

    /// <summary>
    /// Construct a map which is has only spaces and concrete 
    /// </summary>
    public void GenerateBaseMap()
    {
        map.CreateBaseMap();
        pathFinder.GetPath(map);
        DestroyObstacles();
        BuildBlocks();
    }

    /// <summary>
    /// Place obstacles according to basemap
    /// </summary>
    private void BuildBlocks()
    {
        wallOffset = 0.5f + 0.125f;
        int baseVal;
        int wallCoordinate = mapSize - 1;

        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                if (x == 0)
                    Instantiate(wall, new Vector3((float)x - wallOffset, (float)y, 0), Quaternion.Euler(0, 0, 90), mapObject.transform);
                if (x == wallCoordinate)
                    Instantiate(wall, new Vector3((float)x + wallOffset, (float)y, 0), Quaternion.Euler(0, 0, 90), mapObject.transform);
                if (y == 0)
                    Instantiate(wall, new Vector3((float)x, (float)y - wallOffset, 0), Quaternion.identity, mapObject.transform);
                if (y == wallCoordinate)
                    Instantiate(wall, new Vector3((float)x, (float)y + wallOffset, 0), Quaternion.identity, mapObject.transform);

                baseVal = map.baseMap[x, y];
                Instantiate(obstacles[baseVal], new Vector3((float)x, (float)y, 0), this.transform.rotation, mapObject.transform);
            }
        }
    }

    public void Replace(Coordinate position, int obstacleIndex)
    {
        map.baseMap[position.x, position.y] = obstacleIndex;
        DestroyAnObstacle(position.ToVector3());
        Instantiate(obstacles[obstacleIndex], position.ToVector3(), this.transform.rotation, mapObject.transform);
    }

    /// <summary>
    /// Remove all obstacles
    /// </summary>
    private void DestroyObstacles()
    {
        foreach (Transform child in mapObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void DestroyAnObstacle(Vector3 position)
    {
        foreach (Transform child in mapObject.transform)
        {
            if (child.transform.position == position)
            {
                Destroy(child.gameObject);
                return;
            }
        }
    }
}
