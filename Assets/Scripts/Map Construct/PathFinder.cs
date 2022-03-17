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

    public Marker(Coordinate coordinate, Marker parent)
    {
        this.g = 0;
        this.h = 0;
        this.f = this.g + this.h;
        this.coordinate = coordinate;
        this.parent = parent;
    }

    public static void CalculateF(Marker thisMarker, Marker parent, Marker destination)
    {
        thisMarker.g = parent.g + 1;
        thisMarker.h = Vector2.Distance(Coordinate.ToVector2(thisMarker.coordinate), Coordinate.ToVector2(destination.coordinate));
        thisMarker.f = thisMarker.g + thisMarker.h;
    }
}

public class PathFinder : MonoBehaviour
{
    private MapBuilder mapBuilder;
    public static PathFinder singleton;
    private List<Marker> open = new List<Marker>();
    private List<int> closeIndex = new List<int>();
    private LinkedList<Node>[] adjacentList;
    private int mapSize = Map.SIZE;
    private bool isChecking = true;

    void Start()
    {
        singleton = this;
        mapBuilder = GetComponent<GameManager>().mapBuilder;
        adjacentList = mapBuilder.map.adjacentList;
    }

    /// <summary>
    /// Find a path inside the map using A* argorithm
    /// </summary>
    /// <param name="begin">The begin coordinate</param>
    /// <param name="end">The destination coordinate</param>
    /// <returns></returns>
    public Marker FindPath(Coordinate begin, Coordinate end)
    {
        int targetIndex = PathFinder.GetIndexByCoordinate(end);
        Marker beginMarker = new Marker(begin, null);
        Marker endMarker = new Marker(end, null);
        Marker currentMarker;
        open.Add(beginMarker);

        do
        {
            if (open.Count == 0)
            {
                isChecking = false;
                break;
            }

            open = open.OrderBy(mrk => mrk.f).ToList<Marker>();
            currentMarker = open[0];
            open.RemoveAt(0);
            int index = mapSize * currentMarker.coordinate.y + currentMarker.coordinate.x;
            closeIndex.Add(index);

            foreach (Node node in adjacentList[index])
            {
                if (IsClosedIndex(node.index))
                {
                    continue;
                }

                if (node.accessibiliby == Node.IN_ACCESSIBLE_YET)
                {
                    continue;
                }

                if (node.index == targetIndex)
                {
                    endMarker.parent = currentMarker;
                    return endMarker;
                }

                Marker newMarker = new Marker(node.coordinate, currentMarker);
                Marker.CalculateF(newMarker, currentMarker, endMarker);
                open.Add(newMarker);
            }

        } while (isChecking);

        return null;
    }

    private bool IsClosedIndex(int index)
    {
        foreach (int i in closeIndex)
        {
            if (i == index)
            {
                return true;
            }
        }
        return false;
    }

    public static int GetIndexByCoordinate(Coordinate coordinate)
    {
        int targetIndex = Map.SIZE * coordinate.y + coordinate.x;
        return targetIndex;
    }

    private bool GetTrueByRate(int rate)
    {
        int num = Random.Range(1, 101);
        return num < rate ? true : false;
    }

    public Coordinate GetNextCoordinate(Coordinate current)
    {
        int index = PathFinder.GetIndexByCoordinate(current);

        foreach (Node node in adjacentList[index])
        {
            if (node.accessibiliby == Node.ACCESSIBLE)
            {
                if (GetTrueByRate(50))
                {
                    return node.coordinate;
                }
            }
        }
        return null;
    }
}
