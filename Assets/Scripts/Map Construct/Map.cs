using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Coordinate
{
    public int x;
    public int y;
    public static Coordinate right = new Coordinate(0, 1);
    public static Coordinate left = new Coordinate(0, -1);
    public static Coordinate down = new Coordinate(-1, 0);
    public static Coordinate up = new Coordinate(1, 0);

    public static List<Coordinate> directions = new List<Coordinate>(){
        new Coordinate(0, 1),
        new Coordinate(0,-1),
        new Coordinate(1,0),
        new Coordinate(-1,0)
    };

    public Coordinate() { }

    public Coordinate(Vector3 position)
    {
        this.x = (int)position.x;
        this.y = (int)position.y;
    }

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Vector2 ToVector2(Coordinate coordinate)
    {
        return new Vector2((float)coordinate.x, (float)coordinate.y);
    }

    public static Vector3 ToVector3(Coordinate coordinate)
    {
        return new Vector3((float)coordinate.x, (float)coordinate.y, 0);
    }

    public static Coordinate GetCurrentCoordinate(Vector3 position)
    {
        Coordinate currentCoordinate;
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);

        currentCoordinate = new Coordinate(x, y);
        return currentCoordinate;
    }

    public static bool CheckDistance(Coordinate a, Coordinate b, float distanceToCheck)
    {
        Vector3 vectorA = Coordinate.ToVector3(a);
        Vector3 vectorB = Coordinate.ToVector3(b);
        float distance = Vector3.Distance(vectorA, vectorB);

        if (distance <= distanceToCheck)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsInsideMap(Coordinate coordinate, int mapSize)
    {
        if (coordinate.x < 0 || coordinate.x >= mapSize || coordinate.y < 0 || coordinate.y >= mapSize) return false;
        return true;
    }

    public static Coordinate operator +(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.x + b.x, a.y + b.y);
    }

    public bool Compare(Coordinate thisCoordinate)
    {
        if (this.x == thisCoordinate.x && this.y == thisCoordinate.y) return true;
        return false;
    }
}

[Serializable]
public class Map
{
    public string mapName;
    public int[,] baseMap;
    public const int SIZE = 13;
    public List<Coordinate> spaces;
    public LinkedList<Node>[] adjacentList;
    public static Coordinate tower = new Coordinate(6, 0);
    public static Coordinate enemySpawnLeft = new Coordinate(0, 12);
    public static Coordinate enemySpawnMid = new Coordinate(6, 12);
    public static Coordinate enemySpawnRight = new Coordinate(12, 12);
    public static Coordinate playerSpawnLeft = new Coordinate(4, 0);
    public static Coordinate playerSpawnRight = new Coordinate(8, 0);
    public static Coordinate towerWall1 = new Coordinate(5, 0);
    public static Coordinate towerWall2 = new Coordinate(7, 0);
    public static Coordinate towerWall3 = new Coordinate(5, 1);
    public static Coordinate towerWall4 = new Coordinate(6, 1);
    public static Coordinate towerWall5 = new Coordinate(7, 1);

    public Map()
    {
        baseMap = new int[SIZE, SIZE];
        spaces = new List<Coordinate>();
    }

    public void CreateBlank()
    {
        Initialize(false);
    }

    public void CreateBaseMap()
    {
        Initialize(true);
        GeneratePath(new Coordinate(6, 0));
        Debug.Log("finish generate");
    }

    public void Random()
    {
        CreateBaseMap();
        RegenerageMap();
        PlaceTower();
        PlaceTowerWall();
        PlaceSpawnPoint();
    }

    public void PlaceTower()
    {
        SetMap(Map.tower, (int)MapBuilder.TOWER);
    }

    private void PlaceTowerWall()
    {
        SetMap(Map.towerWall1, MapBuilder.BRICK);
        SetMap(Map.towerWall2, MapBuilder.BRICK);
        SetMap(Map.towerWall3, MapBuilder.BRICK);
        SetMap(Map.towerWall4, MapBuilder.BRICK);
        SetMap(Map.towerWall5, MapBuilder.BRICK);
    }

    public void PlaceSpawnPoint()
    {
        SetMap(Map.enemySpawnLeft, MapBuilder.SPACE);
        SetMap(Map.enemySpawnMid, MapBuilder.SPACE);
        SetMap(Map.enemySpawnRight, MapBuilder.SPACE);
        SetMap(Map.playerSpawnLeft, MapBuilder.SPACE);
        SetMap(Map.playerSpawnRight, MapBuilder.SPACE);
    }

    public void SetMap(Coordinate coordinate, int value)
    {
        baseMap[coordinate.x, coordinate.y] = value;
    }

    public void Initialize(bool fill)
    {
        for (int x = 0; x < SIZE; x++)
        {
            for (int y = 0; y < SIZE; y++)
            {
                baseMap[x, y] = Convert.ToInt32(fill);
            }
        }
    }

    private void RegenerageMap()
    {
        Coordinate coordinate;
        spaces.Clear();

        for (int x = 0; x < SIZE; x++)
        {
            for (int y = 0; y < SIZE; y++)
            {
                coordinate = new Coordinate(x, y);
                if (baseMap[x, y] == 0)
                {
                    Change(coordinate, true);
                }

                if (baseMap[x, y] == 1)
                {
                    Change(coordinate, false);
                }
            }
        }
    }

    public void SaveSpaceCoor()
    {
        for (int x = 0; x < SIZE; x++)
        {
            for (int y = 0; y < SIZE; y++)
            {
                if (baseMap[x, y] == MapBuilder.CONCRETE)
                {
                    continue;
                }
                if (baseMap[x, y] == MapBuilder.WATER)
                {
                    continue;
                }
                if (baseMap[x, y] == MapBuilder.TOWER)
                {
                    continue;
                }

                spaces.Add(new Coordinate(x, y));
            }
        }
    }

    private void Change(Coordinate coordinate, bool drillable)
    {
        int randowNumber;
        int[] evens = { 0, 2, 4 };
        int[] odds = { 1, 3 };
        int index;
        if (drillable)
        {
            index = UnityEngine.Random.Range(0, 3);
            randowNumber = evens[index];
        }
        else
        {
            index = UnityEngine.Random.Range(0, 2);
            randowNumber = odds[index];
        }
        SetMap(coordinate, randowNumber);
    }

    private int NeighbourSpace(Coordinate thisCoordinate)
    {
        int spaceCount = 0;
        foreach (Coordinate dir in Coordinate.directions)
        {
            Coordinate neighbour = thisCoordinate + dir;
            if (!Coordinate.IsInsideMap(neighbour, SIZE)) continue;
            if (baseMap[neighbour.x, neighbour.y] == 0) spaceCount++;
        }
        return spaceCount;
    }

    private void GeneratePath(Coordinate begin)
    {
        if (NeighbourSpace(begin) >= 2 || !Coordinate.IsInsideMap(begin, SIZE)) return;

        baseMap[begin.x, begin.y] = 0;

        Coordinate.directions.Shuffle();
        GeneratePath(begin + Coordinate.directions[0]);
        GeneratePath(begin + Coordinate.directions[1]);
        GeneratePath(begin + Coordinate.directions[2]);
        GeneratePath(begin + Coordinate.directions[3]);
    }
}
