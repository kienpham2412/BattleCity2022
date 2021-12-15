using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    public List<GameObject> obstacles = new List<GameObject>();
    public GameObject wall;
    public GameObject mapObject;
    public Map map;
    public int mapSize;
    public float wallOffset;

    // Start is called before the first frame update
    void Start()
    {
        map = new Map();
        map.Random();
        mapSize = map.mapSize;
        BuildBlocks();
        // Debug.Log("obstacle size: " + obstacles[1].GetComponent<SpriteRenderer>().bounds.size.x);
    }

    /// <summary>
    /// Instantiate walls according to base map
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
                    PlaceBorder((float)x, (float)y, "left");
                if (x == wallCoordinate)
                    PlaceBorder((float)x, (float)y, "right");
                if (y == 0)
                    PlaceBorder((float)x, (float)y, "down");
                if (y == wallCoordinate)
                    PlaceBorder((float)x, (float)y, "up");

                baseVal = map.baseMap[x, y];
                Instantiate(obstacles[baseVal], new Vector3((float)x, (float)y, 0), this.transform.rotation, mapObject.transform);
            }
        }
    }

    private void PlaceBorder(float x, float y, string dir)
    {
        switch (dir)
        {
            case "up":
                Instantiate(wall, new Vector3(x, y + wallOffset, 0), Quaternion.identity, mapObject.transform);
                break;
            case "down":
                Instantiate(wall, new Vector3(x, y - wallOffset, 0), Quaternion.identity, mapObject.transform);
                break;
            case "left":
                Instantiate(wall, new Vector3(x - wallOffset, y, 0), Quaternion.Euler(0, 0, 90), mapObject.transform);
                break;
            case "right":
                Instantiate(wall, new Vector3(x + wallOffset, y, 0), Quaternion.Euler(0, 0, 90), mapObject.transform);
                break;
            default:
                break;
        }
    }
}
