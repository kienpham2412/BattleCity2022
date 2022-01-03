using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate
{
    public int x;
    public int y;
    
    public static List<Coordinate> directions = new List<Coordinate>(){
        new Coordinate(0, 1),
        new Coordinate(0,-1),
        new Coordinate(1,0),
        new Coordinate(-1,0)
    };

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
    public List<Coordinate> blocks;
    public Map()
    {
        baseMap = new int[mapSize, mapSize];
    }

    public void CreateBlank(){
        Initialize(false);
    }

    public void CreateBaseMap(){
        Initialize(true);
        GeneratePath(new Coordinate(0,0));
    }

    public void Random(){
        Initialize(true);
        GeneratePath(new Coordinate(0,0));
        RegenerageMap();
        PlaceTower();
    }

    private void PlaceTower()
    {
        SetMap(6, 0, 5);
    }

    public void SetMap(int x, int y, int value)
    {
        baseMap[x, y] = value;
    }

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

    private void RegenerageMap()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                if (baseMap[x, y] == 0)
                    Change(true, x, y);
                    
                if (baseMap[x, y] == 1)
                    Change(false, x, y);
            }
        }
    }

    private void Change(bool drillable, int x, int y)
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
        SetMap(x, y, randowNumber);
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

    // <summary>
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
