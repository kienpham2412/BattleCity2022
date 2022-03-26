using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour
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
    private int currentMapIndex = 0;
    private int spaceIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instance = GetComponent<Referee>();
        gameManager = GetComponent<GameManager>();
        mapNames = gameManager.GetAllMapNames();
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
    }

    public void SpawnTanks()
    {
        tankSpawner.GetClone(Coordinate.ToVector2(Map.enemySpawnLeft), Quaternion.identity);
        tankSpawner.GetClone(Coordinate.ToVector2(Map.enemySpawnMid), Quaternion.identity);
        tankSpawner.GetClone(Coordinate.ToVector2(Map.enemySpawnRight), Quaternion.identity);
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
