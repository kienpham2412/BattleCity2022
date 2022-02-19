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

    public Coordinate(Vector3 position){
        this.x = (int) position.x;
        this.y = (int) position.y;
    }

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Convert coordinate to Vector2
    /// </summary>
    /// <returns></returns>
    public Vector2 ToVector2()
    {
        return new Vector2((float)x, (float)y);
    }

    /// <summary>
    /// Convert coordinate to Vector3
    /// </summary>
    /// <returns></returns>
    public Vector3 ToVector3()
    {
        return new Vector3((float)x, (float)y, 0);
    }

    /// <summary>
    /// Is this coordinate inside the map
    /// </summary>
    /// <returns>true or false</returns>
    public bool IsInsideMap(int mapSize)
    {
        if (x < 0 || x >= mapSize || y < 0 || y >= mapSize) return false;
        return true;
    }

    /// <summary>
    /// Return the sum of 2 coordinates
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Coordinate operator +(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.x + b.x, a.y + b.y);
    }

    public bool Equals(Coordinate thisCoordinate)
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
    public int mapSize = 13;
    public List<Coordinate> spaces;
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
        baseMap = new int[mapSize, mapSize];
        spaces = new List<Coordinate>();
    }

    /// <summary>
    /// Create a blank map
    /// </summary>
    public void CreateBlank()
    {
        Initialize(false);
    }

    /// <summary>
    /// Create a base map with only 0 and 1 value
    /// </summary>
    public void CreateBaseMap()
    {
        Initialize(true);
        GeneratePath(new Coordinate(6, 0));
        Debug.Log("finish generate");
    }

    /// <summary>
    /// Generate a random map
    /// </summary>
    public void Random()
    {
        CreateBaseMap();
        RegenerageMap();
        PlaceTower();
        PlaceTowerWall();
        PlaceSpawnPoint();
    }

    /// <summary>
    /// Place the tower block to the map
    /// </summary>
    public void PlaceTower()
    {
        SetMap(Map.tower, (int)MapBuilder.Block.Tower);
    }

    /// <summary>
    /// Place the tower walls to the map
    /// </summary>
    private void PlaceTowerWall()
    {
        SetMap(Map.towerWall1, (int)MapBuilder.Block.Brick);
        SetMap(Map.towerWall2, (int)MapBuilder.Block.Brick);
        SetMap(Map.towerWall3, (int)MapBuilder.Block.Brick);
        SetMap(Map.towerWall4, (int)MapBuilder.Block.Brick);
        SetMap(Map.towerWall5, (int)MapBuilder.Block.Brick);
    }

    /// <summary>
    /// Place tank spawn points to the map
    /// </summary>
    public void PlaceSpawnPoint()
    {
        SetMap(Map.enemySpawnLeft, (int)MapBuilder.Block.Space);
        SetMap(Map.enemySpawnMid, (int)MapBuilder.Block.Space);
        SetMap(Map.enemySpawnRight, (int)MapBuilder.Block.Space);
        SetMap(Map.playerSpawnLeft, (int)MapBuilder.Block.Space);
        SetMap(Map.playerSpawnRight, (int)MapBuilder.Block.Space);
    }

    /// <summary>
    /// Set a block to the certain coordinate of the map
    /// </summary>
    /// <param name="coordinate">The coordinate of the block</param>
    /// <param name="value">The index of the block</param>
    public void SetMap(Coordinate coordinate, int value)
    {
        baseMap[coordinate.x, coordinate.y] = value;
    }

    /// <summary>
    /// Create a basic map
    /// </summary>
    /// <param name="fill">true if the map is full of concretes and false if the map is empty</param>
    public void Initialize(bool fill)
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                baseMap[x, y] = Convert.ToInt32(fill);
            }
        }
    }

    /// <summary>
    /// Generate a full map from base map
    /// </summary>
    private void RegenerageMap()
    {
        Coordinate coordinate;
        spaces.Clear();

        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
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

    /// <summary>
    /// Saved spaces in the map
    /// </summary>
    public void SaveSpaceCoor()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                if (baseMap[x, y] == (int)MapBuilder.Block.Concrete)
                {
                    continue;
                }
                if (baseMap[x, y] == (int)MapBuilder.Block.Water)
                {
                    continue;
                }
                if (baseMap[x, y] == (int)MapBuilder.Block.Tower)
                {
                    continue;
                }

                spaces.Add(new Coordinate(x, y));
            }
        }
    }

    /// <summary>
    /// Change some blocks of the base map to the new blocks
    /// </summary>
    /// <param name="coordinate"></param>
    /// <param name="drillable"></param>
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

    /// <summary>
    /// Count spaces at the top, down, left, right of current coordinate
    /// </summary>
    /// <param name="thisCoordinate"></param>
    /// <returns></returns>
    private int NeighbourSpace(Coordinate thisCoordinate)
    {
        int spaceCount = 0;
        foreach (Coordinate dir in Coordinate.directions)
        {
            Coordinate neighbour = thisCoordinate + dir;
            if (!neighbour.IsInsideMap(mapSize)) continue;
            if (baseMap[neighbour.x, neighbour.y] == 0) spaceCount++;
        }
        return spaceCount;
    }

    /// <summary>
    /// Generate a path from begin coordinate
    /// </summary>
    /// <param name="begin">The start coordinate</param>
    private void GeneratePath(Coordinate begin)
    {
        if (NeighbourSpace(begin) >= 2 || !begin.IsInsideMap(mapSize)) return;

        baseMap[begin.x, begin.y] = 0;

        Coordinate.directions.Shuffle();
        GeneratePath(begin + Coordinate.directions[0]);
        GeneratePath(begin + Coordinate.directions[1]);
        GeneratePath(begin + Coordinate.directions[2]);
        GeneratePath(begin + Coordinate.directions[3]);
    }
}
