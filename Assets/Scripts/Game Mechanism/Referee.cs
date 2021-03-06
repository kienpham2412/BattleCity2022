using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour, ISubscriber
{
    [Header("Sound")]
    public AudioSource source;
    public AudioClip spawnSFX;

    public static Referee Instance;
    // private GameManager gameManager;
    private MapSaverController mapSaverController;
    private MapBuilder mapBuilder;
    private Map map;
    private IEnumerator itemRoutine;

    [Space]
    [SerializeField]
    private TankSpawner tankSpawner;

    [SerializeField]
    private ItemPooler itemPooler;

    private List<string> mapNames;
    private int spaceIndex = 0;
    private int currentMapIndex;
    public int playerLife = 3;
    public bool isFreeze = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        mapSaverController = MapSaverController.Instance;
        mapNames = mapSaverController.GetAllMapNames();
        currentMapIndex = PlayerPrefs.GetInt("CustomMap", 1);
        Debug.Log("Map index: " + currentMapIndex);

        MessageManager.Instance.AddSubscriber(MessageType.OnGameOver, this);
        MessageManager.Instance.AddSubscriber(MessageType.OnGameFinish, this);
        MessageManager.Instance.AddSubscriber(MessageType.OnPlayerDestroyed, this);
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case MessageType.OnPlayerDestroyed:
                StartCoroutine(RespawnPlayer());
                break;
            case MessageType.OnGameFinish:
                StopSpawningItem();
                break;
            case MessageType.OnGameOver:
                StopSpawningItem();
                break;
        }

    }

    /// <summary>
    /// Load a map to play
    /// </summary>
    public void LoadPlayMap()
    {
        mapSaverController.LoadMap(mapNames[currentMapIndex]);
        currentMapIndex++;
        mapBuilder = mapSaverController.mapBuilder;
        map = mapBuilder.map;
        map.spaces.Shuffle();
        SpawnItem();
        MessageManager.Instance.SendMessage(new Message(MessageType.OnGameRestart));
    }

    public void ChangeToNextStage()
    {
        currentMapIndex++;
    }

    /// <summary>
    /// Check if there are still maps to play or not
    /// </summary>
    /// <returns>True if there are still maps to play</returns>
    public bool CheckIfTheGameEnd()
    {
        if (currentMapIndex == mapNames.Count - 1) return true;
        else return false;
    }

    /// <summary>
    /// Start spawn item coroutine
    /// </summary>
    public void SpawnItem()
    {
        itemRoutine = ItemCoroutine();
        StartCoroutine(itemRoutine);
    }

    /// <summary>
    /// Start the freezing effect
    /// </summary>
    public void StartFreezing(){
        StartCoroutine(Freeze());
    }

    /// <summary>
    /// Stop spawning item
    /// </summary>
    public void StopSpawningItem()
    {
        if (itemRoutine != null)
        {
            StopCoroutine(itemRoutine);
            Debug.Log("Stop spawning item");
        }
    }

    /// <summary>
    /// Spawn enemy and player tank when the game start
    /// </summary>
    public void SpawnTanks()
    {
        tankSpawner.GetEnemyFX(Coordinate.ToVector2(Map.enemySpawnLeft));
        tankSpawner.GetEnemyFX(Coordinate.ToVector2(Map.enemySpawnMid));
        tankSpawner.GetEnemyFX(Coordinate.ToVector2(Map.enemySpawnRight));
        tankSpawner.GetPlayerFX(Coordinate.ToVector2(Map.playerSpawnLeft));
    }

    /// <summary>
    /// Respawn the player after die
    /// </summary>
    /// <returns></returns>
    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(2);
        tankSpawner.GetPlayerFX(Coordinate.ToVector2(Map.playerSpawnLeft));
    }

    /// <summary>
    /// Item spawning coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator ItemCoroutine()
    {
        Debug.Log("Start item coroutine");
        yield return new WaitForSeconds(15f);

        if (spaceIndex >= map.spaces.Count) spaceIndex = 0;

        itemPooler.GetClone(Coordinate.ToVector2(map.spaces[spaceIndex]));
        AudioController.Instance.PlaySFX(source, spawnSFX);
        spaceIndex++;
    }

    /// <summary>
    /// The freeze coroutine
    /// </summary>
    /// <returns></returns>
    IEnumerator Freeze(){
        isFreeze = true;
        yield return new WaitForSeconds(10f);
        isFreeze = false;
    }

    private void OnDisable()
    {
        if (PlayerPrefs.HasKey("CustomMap")) PlayerPrefs.DeleteKey("CustomMap");
        StopAllCoroutines();
    }
}
