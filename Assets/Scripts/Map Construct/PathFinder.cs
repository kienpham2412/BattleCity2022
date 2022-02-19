using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Marker
{
    public Coordinate coordinate;
    public Marker parent;
    public float g, h, f;

    public Marker(float g, float h, Coordinate coordinate, Marker parent)
    {
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
    private MapBuilder mapBuilder;
    public static PathFinder singleton;
    private List<Marker> open = new List<Marker>();
    private List<Coordinate> close = new List<Coordinate>();
    private Map map;
    private bool isFound = false;

    void Start()
    {
        singleton = GetComponent<PathFinder>();
        mapBuilder = GetComponent<GameManager>().mapBuilder;
        map = mapBuilder.map;
    }

    /// <summary>
    /// Find a path inside the map using A* argorithm
    /// </summary>
    /// <param name="begin">The begin coordinate</param>
    /// <param name="end">The destination coordinate</param>
    /// <returns></returns>
    public Marker FindPath(Coordinate begin, Coordinate end)
    {
        Marker beginMarker = new Marker(0, 0, begin, null);
        Marker endMarker = new Marker(0, 0, end, null);
        Marker currentMarker;
        open.Add(beginMarker);

        do
        {
            open = open.OrderBy(mrk => mrk.f).ToList<Marker>();
            currentMarker = open[0];
            open.RemoveAt(0);
            close.Add(currentMarker.coordinate);

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
                    return endMarker;
                }

                Marker newMarker = new Marker(/*marker,*/ 0, 0, neighbour, currentMarker);
                newMarker.CalculateG(currentMarker);
                newMarker.CalculateH(endMarker);
                newMarker.CalculateF();
                open.Add(newMarker);
            }
        } while (!isFound);
        return null;
    }

    /// <summary>
    /// Check if a coordinate is checked in A* argorithm before
    /// </summary>
    /// <param name="thisCoordinate">The coordinate to be checked</param>
    /// <returns>True if this coordinate is checked before and False if not</returns>
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
}
