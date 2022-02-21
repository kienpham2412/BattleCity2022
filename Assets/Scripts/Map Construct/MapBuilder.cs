using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    public const int SPACE = 0;
    public const int CONCRETE = 1;
    public const int BRICK = 2;
    public const int WATER = 3;
    public const int TREE = 4;
    public const int TOWER = 5;
    public List<GameObject> obstacles = new List<GameObject>();
    public GameObject wall;
    public GameObject mapObject;
    public Map map;
    private int mapSize;
    private float wallOffset;

    // Start is called before the first frame update
    void Start()
    {
        map = new Map();
        mapSize = Map.SIZE;
    }

    /// <summary>
    /// Construct a blank map
    /// </summary>
    public void CreateBlankMap()
    {
        map.CreateBlank();
        DestroyObstacles();
        BuildBlocks();
    }

    /// <summary>
    /// Construct a random generated map
    /// </summary>
    public void GenerateRandomMap()
    {
        map.Random();
        DestroyObstacles();
        BuildBlocks();
    }

    /// <summary>
    /// Construct a map which is has only spaces and concrete 
    /// </summary>
    public void GenerateBaseMap()
    {
        map.CreateBaseMap();
        DestroyObstacles();
        BuildBlocks();
    }

    /// <summary>
    /// Place obstacles according to basemap
    /// </summary>
    public void BuildBlocks()
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
                GameObject block = Instantiate(obstacles[baseVal], new Vector3((float)x, (float)y, 0), this.transform.rotation, mapObject.transform);
            }
        }
    }

    /// <summary>
    /// Replace a block to another on the map
    /// </summary>
    /// <param name="position">The coordinate of the block</param>
    /// <param name="obstacleIndex">The new block to be added to the map</param>
    public void Replace(Coordinate coor, int obstacleIndex)
    {
        Vector3 position = Coordinate.ToVector3(coor);
        map.baseMap[coor.x, coor.y] = obstacleIndex;
        DestroyAnObstacle(position);
        Instantiate(obstacles[obstacleIndex], position, this.transform.rotation, mapObject.transform);
    }

    /// <summary>
    /// Build a saved map
    /// </summary>
    /// <param name="map">The map to be loaded</param>
    public void BuildSavedMap(Map map)
    {
        this.map = map;
        this.map.PlaceTower();
        this.map.PlaceSpawnPoint();
        DestroyObstacles();
        BuildBlocks();
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

    /// <summary>
    /// Destroy a block on the map
    /// </summary>
    /// <param name="position">The position of the block to be destroyed</param>
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
