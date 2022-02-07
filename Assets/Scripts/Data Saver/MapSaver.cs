using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MapSaver
{
    static string filePath = Application.dataPath + "/Maps";
    BinaryFormatter bf;
    FileStream file;
    public MapSaver()
    {
        bf = new BinaryFormatter();
    }

    /// <summary>
    /// Save a map
    /// </summary>
    /// <param name="name">Map name</param>
    /// <param name="map">The map</param>
    public void Save(string name, Map map)
    {
        CreateDir();

        map.mapName = name;
        // map.PlaceTower();
        // map.PlaceSpawnPoint();

        file = File.Create(CreatePath(name));
        bf.Serialize(file, map);
        file.Close();
        Debug.Log("map saved !!!");
    }

    /// <summary>
    /// Load a map
    /// </summary>
    /// <param name="name">Map name</param>
    /// <returns>The map</returns>
    public Map Load(string name)
    {
        file = File.Open(CreatePath(name), FileMode.Open);
        Map map = new Map();
        map = (Map)bf.Deserialize(file);
        file.Close();
        Debug.Log("map loaded !!!");
        return map;
    }

    /// <summary>
    /// Create a save directory
    /// </summary>
    private void CreateDir(){
        if(Directory.Exists(filePath)) return;

        Directory.CreateDirectory(filePath);
        Debug.Log("Create directory: Maps");
    }

    /// <summary>
    /// Create save path
    /// </summary>
    /// <param name="name">Map name</param>
    /// <returns></returns>
    private string CreatePath(string name)
    {
        return filePath + "/" + name + ".map";
    }
}
