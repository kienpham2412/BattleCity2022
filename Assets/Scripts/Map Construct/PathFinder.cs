using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Marker
{
    public GameObject marker;
    public Coordinate coordinate;
    public Marker parent;
    public float g, h, f;

    public Marker(GameObject marker, float g, float h, Coordinate coordinate, Marker parent)
    {
        this.marker = marker;
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
    public GameObject begin;
    public GameObject end;
    public GameObject marker;
    private Marker beginMarker;
    private Marker endMarker;
    private Marker currentMarker;
    private PlayerControl myControl;
    private MapBuilder mapBuilder;
    private List<Coordinate> path = new List<Coordinate>();
    private List<Marker> open = new List<Marker>();
    private List<Coordinate> close = new List<Coordinate>();
    private List<Marker> checkedMarker = new List<Marker>();
    private Map map;

    private bool isFound = false;
    private void Awake()
    {
        myControl = new PlayerControl();
        myControl.MapBuilding.GenerateNode.performed += ctx => GenerateNodes();
        myControl.MapBuilding.GeneratePath.performed += ctx => FindPath();
        myControl.MapBuilding.ShowShortestPath.performed += ctx => ShowPath();
    }
    // Start is called before the first frame update
    void Start()
    {
        mapBuilder = GetComponent<MapBuilder>();
        map = mapBuilder.map;
        GetPath();
    }

    public void GetPath()
    {
        path.Clear(); // error found
        for (int x = 0; x < map.mapSize; x++)
        {
            for (int y = 0; y < map.mapSize; y++)
            {
                if (map.baseMap[x, y] == 1) continue;
                if (map.baseMap[x, y] == 3) continue;
                if (map.baseMap[x, y] == 5) continue;
                path.Add(new Coordinate(x, y));
            }
        }
    }

    private void GenerateNodes()
    {
        path.Shuffle();
        RemoveMarkers();

        beginMarker = new Marker(begin, 0, 0, path[0], null);
        beginMarker.Instantiate();
        endMarker = new Marker(end, 0, 0, path[1], null);
        endMarker.Instantiate();

        beginMarker.CalculateH(endMarker);
        beginMarker.CalculateF();
        open.Add(beginMarker);

        isFound = false;
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

    private void FindPath()
    {
        if (isFound) return;

        open = open.OrderBy(mrk => mrk.f).ToList<Marker>();
        currentMarker = open[0];
        open.RemoveAt(0);
        close.Add(currentMarker.coordinate);
        Debug.Log("current marker at: " + currentMarker.coordinate.x + "-" + currentMarker.coordinate.y);

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
                Debug.Log("Goal found");
                return;
            }

            Marker newMarker = new Marker(marker, 0, 0, neighbour, currentMarker);
            newMarker.CalculateG(currentMarker);
            newMarker.CalculateH(endMarker);
            newMarker.CalculateF();
            newMarker.Instantiate();
            open.Add(newMarker);
        }
    }

    private void ShowPath()
    {
        if (!isFound) return;

        RemoveMarkers();
        Marker cur = endMarker;
        while (cur.parent != null)
        {
            cur.Instantiate();
            cur = cur.parent;
        }
        beginMarker.Instantiate();

        checkedMarker.Clear();
    }

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

    private void OnEnable()
    {
        myControl.MapBuilding.Enable();
    }

    private void OnDisable()
    {
        myControl.MapBuilding.Disable();
    }
}
