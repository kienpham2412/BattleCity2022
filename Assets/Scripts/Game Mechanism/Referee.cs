using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour
{
    public static Referee singleton;
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

        GameManager.singleton.LoadMap("Blank");
        mapBuilder = GameManager.singleton.mapBuilder;
        map = mapBuilder.map;
        map.spaces.Shuffle();

        StartCoroutine(SpawnEnemy());
        SpawnItem();
    }

    // Update is called once per frame
    // void Update()
    // {

    // }

    public void SpawnClExplosion(Vector2 position)
    {
        particalCtrl.GetClone(position, Quaternion.identity, ParticalController.Partical.Collision);
    }

    public void SpawnDestroyExplosion(Vector2 position)
    {
        particalCtrl.GetClone(position, Quaternion.identity, ParticalController.Partical.Destroy);
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

    public void DestroyEnemies(){
        tankSpawner.DestroyActiveTank();
    }

    public void FreezeEnemies(){
        tankSpawner.FreezeActiveTank();
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2f);
        tankSpawner.GetClone(Map.enemySpawnLeft.ToVector2(), Quaternion.identity);
        // tankSpawner.GetClone(Map.enemySpawnMid.ToVector2(), Quaternion.identity);
        // tankSpawner.GetClone(Map.enemySpawnRight.ToVector2(), Quaternion.identity);
        tankSpawner.GetPlayerFX(Map.playerSpawnLeft.ToVector2());
    }

    IEnumerator ItemCoroutine()
    {
        Debug.Log("Start item coroutine");
        yield return new WaitForSeconds(30f);

        if (spaceIndex >= map.spaces.Count)
        {
            spaceIndex = 0;
        }

        itemPooler.GetClone(map.spaces[spaceIndex].ToVector2());
        spaceIndex++;
    }
}
