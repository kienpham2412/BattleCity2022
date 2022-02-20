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

    // Update is called once per frame
    // void Update()
    // {

    // }

    /// <summary>
    /// Spawn collision explosion effect on the map
    /// </summary>
    /// <param name="position">The position to spawn the effect</param>
    public void SpawnClExplosion(Vector2 position)
    {
        particalCtrl.GetClone(position, ParticalController.Partical.Collision);
    }

    /// <summary>
    /// Spawn destroy explosion effect on the map
    /// </summary>
    /// <param name="position">The position to spawn the effect</param>
    public void SpawnDestroyExplosion(Vector2 position)
    {
        particalCtrl.GetClone(position, ParticalController.Partical.Destroy);
    }

    /// <summary>
    /// Get enemy clone
    /// </summary>
    /// <param name="position">The position to place the enemy</param>
    public void GetEnemyClone(Vector2 position)
    {
        tankSpawner.GetEnemyClone(position);
    }

    /// <summary>
    /// Get player clone
    /// </summary>
    /// <param name="position">The position to place the player</param>
    public void GetPlayerClone(Vector2 position)
    {
        tankSpawner.SpawnPlayer(position);
    }

    /// <summary>
    /// Spawn an item on the map
    /// </summary>
    public void SpawnItem()
    {
        StartCoroutine(ItemCoroutine());
    }

    /// <summary>
    /// Destroy all enemies
    /// </summary>
    public void DestroyEnemies()
    {
        tankSpawner.DestroyActiveTank();
    }

    /// <summary>
    /// Freeze all enemies
    /// </summary>
    public void FreezeEnemies()
    {
        tankSpawner.FreezeActiveTank();
    }

    /// <summary>
    /// Spawn the tanks after a certain of time
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2f);
        tankSpawner.GetClone(Coordinate.ToVector2(Map.enemySpawnLeft), Quaternion.identity);
        // tankSpawner.GetClone(Map.enemySpawnMid.ToVector2(), Quaternion.identity);
        // tankSpawner.GetClone(Map.enemySpawnRight.ToVector2(), Quaternion.identity);
        tankSpawner.GetPlayerFX(Coordinate.ToVector2(Map.playerSpawnLeft));
    }

    /// <summary>
    /// Spawn an item after a certain of time
    /// </summary>
    /// <returns></returns>
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
