using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour
{
    public static Referee singleton;
    private GameManager gameManager;
    private MapBuilder mapBuilder;
    private Map map;

    [SerializeField]
    private ParticalController particalCtrl;

    [SerializeField]
    private TankSpawner tankSpawner;

    [SerializeField]
    private ItemPooler itemPooler;

    private int spaceIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        singleton = GetComponent<Referee>();
        gameManager = GetComponent<GameManager>();
        gameManager.LoadMap("Base");
        
        mapBuilder = gameManager.mapBuilder;
        map = mapBuilder.map;
        map.spaces.Shuffle();

        StartCoroutine(SpawnEnemy());
        SpawnItem();
    }

    public void SpawnClExplosion(Vector2 position)
    {
        particalCtrl.GetClone(position, ParticalController.Partical.Collision);
    }

    public void SpawnDestroyExplosion(Vector2 position)
    {
        particalCtrl.GetClone(position, ParticalController.Partical.Destroy);
    }

    public void GetEnemyClone(Vector2 position)
    {
        tankSpawner.GetEnemyClone(position);
    }

    public void GetPlayerClone(Vector2 position)
    {
        tankSpawner.SpawnPlayer(position);
    }

    public void SpawnItem()
    {
        StartCoroutine(ItemCoroutine());
    }

    public void DestroyEnemies()
    {
        tankSpawner.DestroyActiveTank();
    }

    public void FreezeEnemies()
    {
        tankSpawner.FreezeActiveTank();
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2f);
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
