using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Node
{
    public const int ACCESSIBLE = 2;
    public const int IN_ACCESSIBLE_YET = 1;
    public const int IN_ACCESSIBLE = 0;
    public Coordinate coordinate;
    public int index;
    public int accessibiliby;

    public Node(){}

    public Node(Coordinate coordinate, int accessibiliby)
    {
        this.index = Map.SIZE * coordinate.y + coordinate.x;
        this.coordinate = coordinate;
        this.accessibiliby = accessibiliby;
    }
}

public class Graph
{
    private int[,] baseMap;
    private LinkedList<Node>[] adjacentList;
    private int mapSize;
    private int length;

    public Graph(int[,] baseMap)
    {
        this.baseMap = baseMap;
        mapSize = Map.SIZE;
        length = mapSize * mapSize;
    }

    private void AddEdge(Coordinate first, Coordinate second)
    {
        int firstAccessibility = GetAccessibility(first);
        int secondAccessibility = GetAccessibility(second);
        Node firstNode = new Node(first, firstAccessibility);
        Node secondNode = new Node(second, secondAccessibility);
        Creategraph(firstNode.index);
        Creategraph(secondNode.index);

        if (firstAccessibility == Node.IN_ACCESSIBLE)
        {
            return;
        }
        if (secondAccessibility == Node.IN_ACCESSIBLE)
        {
            return;
        }

        adjacentList[firstNode.index].AddLast(secondNode);
        adjacentList[secondNode.index].AddLast(firstNode);
    }

    public LinkedList<Node>[] BuildGraph()
    {
        adjacentList = new LinkedList<Node>[length];

        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                Coordinate coordinate = new Coordinate(x, y);
                Coordinate up = coordinate + Coordinate.up;
                Coordinate right = coordinate + Coordinate.right;
                if (Coordinate.IsInsideMap(up, mapSize))
                {
                    AddEdge(coordinate, coordinate + Coordinate.up);
                }
                if (Coordinate.IsInsideMap(right, mapSize))
                {
                    AddEdge(coordinate, coordinate + Coordinate.right);
                }
            }
        }
        return adjacentList;
    }

    private void Creategraph(int nodeIndex)
    {
        if (adjacentList[nodeIndex] == null)
        {
            adjacentList[nodeIndex] = new LinkedList<Node>();
        }
    }

    public static void PrintGraph(LinkedList<Node>[] graph)
    {
        int length = graph.Length;
        Debug.Log("Graph for this map: ");
        for (int i = 0; i < length; i++)
        {
            string ouput = $"node {i}";
            foreach (Node node in graph[i])
            {
                ouput += $" -> {node.index} - {node.accessibiliby}";
            }
            Debug.Log(ouput);
        }
    }

    private int GetAccessibility(Coordinate coordinate)
    {
        int block = baseMap[coordinate.x, coordinate.y];
        if (block == MapBuilder.WATER)
        {
            return Node.IN_ACCESSIBLE;
        }

        if (block == MapBuilder.CONCRETE)
        {
            return Node.IN_ACCESSIBLE_YET;
        }

        return Node.ACCESSIBLE;
    }
}
