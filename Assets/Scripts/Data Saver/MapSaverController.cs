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

    public Map Load(string name)
    {
        file = File.Open(CreatePath(name), FileMode.Open);
        Map map = new Map();
        map = (Map)bf.Deserialize(file);
        file.Close();
        Debug.Log("map loaded !!!");
        return map;
    }

    private void CreateDir()
    {
        if (Directory.Exists(filePath)) return;

        Directory.CreateDirectory(filePath);
        Debug.Log("Create directory: Maps");
    }

    private string CreatePath(string name)
    {
        return filePath + "/" + name + ".map";
    }

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
    }

    public void SaveMap(string name)
    {
        mapBuilder.map.SaveSpaceCoor();
        mapSaver.Save(name, mapBuilder.map);
    }

    public void LoadMap(string name)
    {
        mapBuilder.BuildSavedMap(mapSaver.Load(name));
    }

    public List<string> GetAllMapNames()
    {
        return mapSaver.GetFilesName();
    }

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
