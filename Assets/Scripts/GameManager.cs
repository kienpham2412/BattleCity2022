using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private MapSaver mapSaver;
    public MapBuilder mapBuilder;
    // Start is called before the first frame update
    void Start()
    {
        Instance = GetComponent<GameManager>();
        mapSaver = new MapSaver();
    }

    /// <summary>
    /// Save the map
    /// </summary>
    /// <param name="name">Map name</param>
    public void SaveMap(string name){
        mapBuilder.map.SaveSpaceCoor();
        mapSaver.Save(name, mapBuilder.map);
    }

    /// <summary>
    /// Load the map
    /// </summary>
    /// <param name="name">Map name</param>
    public void LoadMap(string name){
        mapBuilder.BuildSavedMap(mapSaver.Load(name));
    }
}
