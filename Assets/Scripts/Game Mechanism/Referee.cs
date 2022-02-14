using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour
{
    public static Referee singleton;
    private MapBuilder mapBuilder;

    [SerializeField]
    private ParticalController particalCtrl;
    
    // Start is called before the first frame update
    void Start()
    {
        singleton = GetComponent<Referee>();

        GameManager.singleton.LoadMap("7222");
        mapBuilder = GameManager.singleton.mapBuilder;
        mapBuilder.Replace(Map.tower, 5);
        mapBuilder.Replace(Map.enemySpawnLeft, 0);
        mapBuilder.Replace(Map.enemySpawnMid, 0);
        mapBuilder.Replace(Map.enemySpawnRight, 0);
        mapBuilder.Replace(Map.playerSpawnLeft, 0);
        mapBuilder.Replace(Map.playerSpawnRignt, 0);

        EnemySpawner.singleton.GetClone(Map.enemySpawnLeft.ToVector2(), Quaternion.identity);
        EnemySpawner.singleton.GetClone(Map.enemySpawnMid.ToVector2(), Quaternion.identity);
        EnemySpawner.singleton.GetClone(Map.enemySpawnRight.ToVector2(), Quaternion.identity);
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
}
