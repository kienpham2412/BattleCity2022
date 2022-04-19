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

    /// <summary>
    /// Calculate F value of heuristic equation
    /// </summary>
    /// <param name="thisMarker">The marker that is being calculated</param>
    /// <param name="parent">The parent of this marker</param>
    /// <param name="destination">The destination marker</param>
    public static void CalculateF(Marker thisMarker, Marker parent, Marker destination)
    {
        thisMarker.g = parent.g + 1;
        thisMarker.h = Vector2.Distance(Coordinate.ToVector2(thisMarker.coordinate), Coordinate.ToVector2(destination.coordinate));
        thisMarker.f = thisMarker.g + thisMarker.h;
    }
}

public class PathFinder : MonoBehaviour, ISubscriber
{
    private MapBuilder mapBuilder;
    private List<Marker> open = new List<Marker>();
    private List<int> closeIndex = new List<int>();
    private LinkedList<Node>[] adjacentList;
    public const float UNLIMITED = 30;
    public const float TOWER_RANGE = 4;
    public const float PLAYER_RANGE = 5;
    private bool isChecking = true;

    void Start()
    {
        mapBuilder = MapSaverController.Instance.mapBuilder;
        MessageManager.Instance.AddSubscriber(MessageType.OnGameRestart, this);
    }

    public void Handle(Message message)
    {
        adjacentList = mapBuilder.map.adjacentList;
    }

    /// <summary>
    /// Find a path from start position to destination
    /// </summary>
    /// <param name="start">Start coordinate</param>
    /// <param name="destination">Destination coordinate</param>
    /// <param name="limitRange">The limit distance from the calculated coordinate to destination coordinate</param>
    /// <returns>The begining marker of the path to the destination</returns>
    public Marker FindPath(Coordinate start, Coordinate destination, float limitRange)
    {
        int targetIndex = PathFinder.GetIndexByCoordinate(start);
        Marker beginMarker = new Marker(destination, null);
        Marker endMarker = new Marker(start, null);
        Marker currentMarker;

        open.Clear();
        closeIndex.Clear();
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
            int index = PathFinder.GetIndexByCoordinate(currentMarker.coordinate);
            closeIndex.Add(index);

            foreach (Node node in adjacentList[index])
            {
                if (IsClosedIndex(node.index))
                {
                    continue;
                }

                if (!Coordinate.CheckDistance(destination, node.coordinate, limitRange))
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

    /// <summary>
    /// Is the marker calculated
    /// </summary>
    /// <param name="index">Index value of the node</param>
    /// <returns>True if this node is calculated</returns>
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

    /// <summary>
    /// Get the index of the node using coordinate
    /// </summary>
    /// <param name="coordinate">The coordinate of the node</param>
    /// <returns>The index value of the node</returns>
    public static int GetIndexByCoordinate(Coordinate coordinate)
    {
        int targetIndex = Map.SIZE * coordinate.y + coordinate.x;
        return targetIndex;
    }

    /// <summary>
    /// Get true value by specific rate
    /// </summary>
    /// <param name="rate">The rate value</param>
    /// <returns></returns>
    private bool GetTrueByRate(int rate)
    {
        int num = Random.Range(1, 101);
        return num < rate ? true : false;
    }

    /// <summary>
    /// Randomly choose the next coordinate from the specific coordinate
    /// </summary>
    /// <param name="current">The specific coordinate</param>
    /// <param name="previous">The last coordinate to be ignored</param>
    /// <returns></returns>
    public Coordinate GetNextCoordinate(Coordinate current, ref Coordinate previous)
    {
        int index = PathFinder.GetIndexByCoordinate(current);

        foreach (Node node in adjacentList[index])
        {
            if (node.accessibiliby == Node.ACCESSIBLE)
            {
                if (node.coordinate.Compare(previous)) continue;

                if (GetTrueByRate(85))
                {
                    previous = current;
                    return node.coordinate;
                }
            }
        }

        previous = current;
        return null;
    }
}
