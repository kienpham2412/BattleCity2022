using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour
{
    public static Referee Instance;
    private GameManager gameManager;
    private MapBuilder mapBuilder;
    private PathFinder pathFinder;
    private Map map;

    [SerializeField]
    private TankSpawner tankSpawner;

    [SerializeField]
    private ItemPooler itemPooler;

    private int spaceIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instance = GetComponent<Referee>();
        gameManager = GetComponent<GameManager>();
        pathFinder = GetComponent<PathFinder>();
    }

    public void LoadPlayMap(){
        gameManager.LoadMap("Base");
        mapBuilder = gameManager.mapBuilder;
        map = mapBuilder.map;
        map.spaces.Shuffle();
        pathFinder.LoadGraph();
        SpawnItem();
    }

    public void SpawnItem()
    {
        StartCoroutine(ItemCoroutine());
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
