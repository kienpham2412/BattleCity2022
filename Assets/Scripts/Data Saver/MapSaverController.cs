using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class MapSaver
{
    static string filePath = Application.streamingAssetsPath + "/Maps";
    BinaryFormatter bf;
    FileStream file;
    private Graph graph;
    public MapSaver()
    {
        bf = new BinaryFormatter();
    }

    /// <summary>
    /// Save the map
    /// </summary>
    /// <param name="name">Map name</param>
    /// <param name="map">Map data</param>
    public void Save(string name, Map map)
    {
        CreateDir();

        map.mapName = name;
        graph = new Graph(map.baseMap);
        map.adjacentList = graph.BuildGraph();

        file = File.Create(CreatePath(name));
        bf.Serialize(file, map);
        file.Close();
        Debug.Log("map saved !!!");
    }

    /// <summary>
    /// Load map by name
    /// </summary>
    /// <param name="name">Map name</param>
    /// <returns></returns>
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
    /// Create folder for the maps
    /// </summary>
    private void CreateDir()
    {
        if (Directory.Exists(filePath)) return;

        Directory.CreateDirectory(filePath);
        Debug.Log("Create directory: Maps");
    }

    /// <summary>
    /// Create a directory to the map folder
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private string CreatePath(string name)
    {
        return filePath + "/" + name + ".map";
    }

    /// <summary>
    /// Get map name
    /// </summary>
    /// <returns></returns>
    public List<string> GetFilesName()
    {
        DirectoryInfo dirInfo = new DirectoryInfo(filePath);
        FileInfo[] files = dirInfo.GetFiles("*.map");
        List<string> mapNames = new List<string>();
        foreach (FileInfo f in files)
        {
            string name = f.Name.Replace(".map", "");
            mapNames.Add(name);
        }
        return mapNames;
    }
}

public class MapSaverController : MonoBehaviour
{
    public static MapSaverController Instance;
    private MapSaver mapSaver;
    public MapBuilder mapBuilder;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        mapSaver = new MapSaver();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// Save map API
    /// </summary>
    /// <param name="name"></param>
    public void SaveMap(string name)
    {
        mapBuilder.map.SaveSpaceCoor();
        mapSaver.Save(name, mapBuilder.map);
    }

    /// <summary>
    /// Load map API
    /// </summary>
    /// <param name="name"></param>
    public void LoadMap(string name)
    {
        mapBuilder.BuildSavedMap(mapSaver.Load(name));
    }

    /// <summary>
    /// Get all map name 
    /// </summary>
    /// <returns></returns>
    public List<string> GetAllMapNames()
    {
        return mapSaver.GetFilesName();
    }

    /// <summary>
    /// Handle scene load event
    /// </summary>
    /// <param name="scene">The current scene</param>
    /// <param name="mode">Load scene mode</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("TitleScreen")) return;

        mapBuilder = FindObjectOfType<MapBuilder>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
