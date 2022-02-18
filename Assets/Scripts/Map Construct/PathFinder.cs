using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Marker
{
    // public GameObject marker;
    public Coordinate coordinate;
    public Marker parent;
    public float g, h, f;

    public Marker(/*GameObject marker,*/ float g, float h, Coordinate coordinate, Marker parent)
    {
        // this.marker = marker;
        this.g = g;
        this.h = h;
        this.f = this.g + this.h;
        this.coordinate = coordinate;
        this.parent = parent;
    }

    public void CalculateG(Marker parent)
    {
        this.g = parent.g + 1;
    }

    public void CalculateH(Marker destination)
    {
        this.h = Vector2.Distance(this.coordinate.ToVector2(), destination.coordinate.ToVector2());
    }

    public void CalculateF()
    {
        f = g + h;
    }
}

public class PathFinder : MonoBehaviour
{
    // public GameObject begin;
    // public GameObject end;
    // public GameObject marker;
    // private Marker beginMarker;
    // private Marker endMarker;
    // private Marker currentMarker;

    // private PlayerControl myControl;
    // private List<Coordinate> path = new List<Coordinate>();
    private MapBuilder mapBuilder;
    public static PathFinder singleton;
    private List<Marker> open = new List<Marker>();
    private List<Coordinate> close = new List<Coordinate>();
    // private List<Marker> checkedMarker = new List<Marker>();
    private Map map;
    private bool isFound = false;

    // private void Awake()
    // {
    //     // myControl = new PlayerControl();
    //     // myControl.MapBuilding.GenerateNode.performed += ctx => GenerateNodes();
    //     // myControl.MapBuilding.GeneratePath.performed += ctx => FindPath();
    //     // myControl.MapBuilding.ShowShortestPath.performed += ctx => ShowPath();
    // }

    // public void GetPath(Map map)
    // {
    //     this.map = map;
    //     path.Clear(); // error found
    //     for (int x = 0; x < map.mapSize; x++)
    //     {
    //         for (int y = 0; y < map.mapSize; y++)
    //         {
    //             if (map.baseMap[x, y] == 1) continue;
    //             if (map.baseMap[x, y] == 3) continue;
    //             if (map.baseMap[x, y] == 5) continue;
    //             path.Add(new Coordinate(x, y));
    //         }
    //     }
    // }

    // private void GenerateNodes()
    // {
    //     path.Shuffle();
    //     RemoveMarkers();

    //     beginMarker = new Marker(begin, 0, 0, path[0], null);
    //     beginMarker.Instantiate();
    //     endMarker = new Marker(end, 0, 0, path[1], null);
    //     endMarker.Instantiate();

    //     beginMarker.CalculateH(endMarker);
    //     beginMarker.CalculateF();
    //     open.Add(beginMarker);

    //     isFound = false;
    // }

    void Start()
    {
        singleton = GetComponent<PathFinder>();
        mapBuilder = GetComponent<GameManager>().mapBuilder;
        map = mapBuilder.map;
        Debug.Log($"size: {map.mapSize}");

        // FindPath(Map.tower, Map.enemySpawnLeft);
    }

    private void RemoveMarkers()
    {
        GameObject[] markers = GameObject.FindGameObjectsWithTag("Marker");
        if (markers != null)
        {
            foreach (GameObject marker in markers)
            {
                Destroy(marker);
            }
        }

        open.Clear();
        close.Clear();
    }

    public Marker FindPath(Coordinate begin, Coordinate end)
    {
        Marker beginMarker = new Marker(0, 0, begin, null);
        Marker endMarker = new Marker(0, 0, end, null);
        Marker currentMarker;
        open.Add(beginMarker);

        // if (isFound) return;
        do
        {
            open = open.OrderBy(mrk => mrk.f).ToList<Marker>();
            currentMarker = open[0];
            open.RemoveAt(0);
            // Debug.Log($"current marker: {currentMarker.coordinate.x} - {currentMarker.coordinate.y}");
            close.Add(currentMarker.coordinate);
            // Debug.Log("current marker at: " + currentMarker.coordinate.x + "-" + currentMarker.coordinate.y);

            foreach (Coordinate coor in Coordinate.directions)
            {
                Coordinate neighbour = currentMarker.coordinate + coor;
                if (!neighbour.IsInsideMap(map.mapSize)) continue;
                if (map.baseMap[neighbour.x, neighbour.y] == 1) continue;
                if (map.baseMap[neighbour.x, neighbour.y] == 3) continue;
                if (map.baseMap[neighbour.x, neighbour.y] == 5) continue;
                if (IsClosed(neighbour)) continue;
                if (neighbour.Equals(endMarker.coordinate))
                {
                    endMarker.parent = currentMarker;
                    isFound = true;
                    // Debug.Log($"end marker: {endMarker.coordinate.x} - {endMarker.coordinate.y}");
                    return endMarker;
                }

                Marker newMarker = new Marker(/*marker,*/ 0, 0, neighbour, currentMarker);
                newMarker.CalculateG(currentMarker);
                newMarker.CalculateH(endMarker);
                newMarker.CalculateF();
                // newMarker.Instantiate();
                open.Add(newMarker);
            }
        } while (!isFound);
        return null;
        // ShowPath();
    }

    // private void ShowPath()
    // {
    //     if (!isFound) return;

    //     RemoveMarkers();
    //     Marker cur = endMarker;
    //     while (cur.parent != null)
    //     {
    //         cur.Instantiate();
    //         cur = cur.parent;
    //     }
    //     beginMarker.Instantiate();

    //     checkedMarker.Clear();
    // }

    private bool IsClosed(Coordinate thisCoordinate)
    {
        foreach (Coordinate coor in close)
        {
            if (thisCoordinate.Equals(coor))
            {
                return true;
            }
        }
        return false;
    }

    // private void OnEnable()
    // {
    //     myControl.MapBuilding.Enable();
    // }

    // private void OnDisable()
    // {
    //     myControl.MapBuilding.Disable();
    // }
}
