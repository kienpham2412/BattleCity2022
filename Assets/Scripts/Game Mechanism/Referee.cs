using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour, ISubscriber
{
    public static Referee Instance;
    private GameManager gameManager;
    private MapBuilder mapBuilder;
    private Map map;
    private IEnumerator itemRoutine;

    [SerializeField]
    private TankSpawner tankSpawner;

    [SerializeField]
    private ItemPooler itemPooler;

    private List<string> mapNames;
    private int spaceIndex = 0;
    private int currentMapIndex;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        gameManager = GetComponent<GameManager>();
        mapNames = gameManager.GetAllMapNames();
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
                StopAllCoroutines();
                break;
            case MessageType.OnGameOver:
                StopAllCoroutines();
                break;
        }

    }

    public void LoadPlayMap()
    {
        gameManager.LoadMap(mapNames[currentMapIndex]);
        currentMapIndex++;
        mapBuilder = gameManager.mapBuilder;
        map = mapBuilder.map;
        map.spaces.Shuffle();
        SpawnItem();
        MessageManager.Instance.SendMessage(new Message(MessageType.OnGameRestart));
    }

    public void ChangeToNextStage()
    {
        currentMapIndex++;
    }

    public bool CheckIfTheGameEnd()
    {
        if (currentMapIndex == mapNames.Count - 1) return true;
        else return false;
    }

    public void SpawnItem()
    {
        itemRoutine = ItemCoroutine();
        StartCoroutine(itemRoutine);
    }

    public void StopSpawningItem()
    {
        if (itemRoutine != null)
        {
            StopCoroutine(itemRoutine);
        }
        Debug.Log("Stop spawning item");
    }

    public void SpawnTanks()
    {
        tankSpawner.GetClone(Coordinate.ToVector2(Map.enemySpawnLeft), Quaternion.identity);
        tankSpawner.GetClone(Coordinate.ToVector2(Map.enemySpawnMid), Quaternion.identity);
        tankSpawner.GetClone(Coordinate.ToVector2(Map.enemySpawnRight), Quaternion.identity);
        tankSpawner.GetPlayerFX(Coordinate.ToVector2(Map.playerSpawnLeft));
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(2);
        tankSpawner.GetPlayerFX(Coordinate.ToVector2(Map.playerSpawnLeft));
    }

    IEnumerator ItemCoroutine()
    {
        Debug.Log("Start item coroutine");
        yield return new WaitForSeconds(30f);

        if (spaceIndex >= map.spaces.Count)
        {
            spaceIndex = 0;
        }

        itemPooler.GetClone(Coordinate.ToVector2(map.spaces[spaceIndex]));
        spaceIndex++;
    }
}
