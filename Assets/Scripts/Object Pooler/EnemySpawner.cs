using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ObjectPooler
{
    public static EnemySpawner singleton;
    private List<GameObject> enemyList, spawnFXList;
    private int enemyIndex = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        singleton = GetComponent<EnemySpawner>();
        enemyList = new List<GameObject>();
        spawnFXList = new List<GameObject>();

        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < 20; i++)
        {
            if (i <= 2)
            {
                Clone(samples[0], spawnFXList);
            }

            Clone(samples[1], enemyList);
        }
    }

    public override GameObject GetClone(Vector2 position, Quaternion rotation)
    {
        foreach (GameObject gameObj in spawnFXList)
        {
            if (!gameObj.activeSelf)
            {
                gameObj.transform.position = position;
                gameObj.transform.rotation = rotation;
                gameObj.SetActive(true);

                return gameObj;
            }
        }
        return null;
    }

    public void GetEnemyClone(Vector2 position)
    {
        GameObject gameObj = enemyList[enemyIndex];
        gameObj.transform.position = position;
        gameObj.transform.rotation = Quaternion.Euler(0, 0, 180);
        gameObj.SetActive(true);

        enemyIndex++;
    }
}
