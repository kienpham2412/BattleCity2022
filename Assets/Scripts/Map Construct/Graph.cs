using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public const int ACCESSIBLE = 2;
    public const int IN_ACCESSIBLE_YET = 1;
    public const int IN_ACCESSIBLE = 0;
    public int index;
    public Coordinate coordinate;
    public int accessibiliby;

    public Node(Coordinate coordinate, int accessibiliby)
    {
        this.index = Map.SIZE * coordinate.y + coordinate.x;
        this.coordinate = coordinate;
        this.accessibiliby = accessibiliby;
    }
}

public class Graph : MonoBehaviour
{
    private Map map;
    private LinkedList<Node>[] graph;
    private int mapSize;
    private int arrayLength;
    
    // Start is called before the first frame update
    void Start()
    {
        map = GetComponent<GameManager>().mapBuilder.map;
        mapSize = Map.SIZE;
        arrayLength = mapSize * mapSize;

        // CreateGraph();
        BuildGraph();
        Graph.PrintGraph(graph);
        Debug.Log("successful");
    }

    private void AddEdge(Coordinate first, Coordinate second)
    {
        int firstAccessibility = GetAccessibility(first);
        int secondAccessibility = GetAccessibility(second);
        Node firstNode = new Node(first, firstAccessibility);
        Node secondNode = new Node(second, secondAccessibility);
        CreateAdjacentList(firstNode.index);
        CreateAdjacentList(secondNode.index);

        if (firstAccessibility == Node.IN_ACCESSIBLE)
        {
            return;
        }
        if (secondAccessibility == Node.IN_ACCESSIBLE)
        {
            return;
        }

        graph[firstNode.index].AddLast(secondNode);
        graph[secondNode.index].AddLast(firstNode);
    }

    // private void CreateGraph()
    // {
    //     graph = new LinkedList<Node>[arrayLength];
    //     for (int i = 0; i < arrayLength; i++)
    //     {
    //         graph[i] = new LinkedList<Node>();
    //     }
    // }

    private void BuildGraph()
    {
        graph = new LinkedList<Node>[arrayLength];

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
    }

    private void CreateAdjacentList(int nodeIndex)
    {
        if (graph[nodeIndex] == null)
        {
            graph[nodeIndex] = new LinkedList<Node>();
        }
    }

    public static void PrintGraph(LinkedList<Node>[] graph)
    {
        int arrayLength = graph.Length;
        Debug.Log("Graph for this map: ");
        for (int i = 0; i < arrayLength; i++)
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
        int block = map.baseMap[coordinate.x, coordinate.y];
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
