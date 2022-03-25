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
        mapBuilder = GetComponent<GameManager>().mapBuilder;
        MessageManager.Instance.AddSubscriber(MessageType.OnGameRestart, this);
    }

    public void Handle(Message message)
    {
        adjacentList = mapBuilder.map.adjacentList;
    }

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
