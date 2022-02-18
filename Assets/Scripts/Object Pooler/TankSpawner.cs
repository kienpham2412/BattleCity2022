using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : ObjectPooler
{
    private List<GameObject> enemyList, spawnFXList;
    private GameObject playerSpawnFX, playerTank;
    private int enemyIndex = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        enemyList = new List<GameObject>();
        spawnFXList = new List<GameObject>();

        CreatePlayer();
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < 20; i++)
        {
            if (i <= 4)
            {
                Clone(samples[2], spawnFXList);
            }

            Clone(samples[3], enemyList);
        }
    }

    private void CreatePlayer()
    {
        playerSpawnFX = Instantiate(samples[0], new Vector2(0, 0), Quaternion.identity);
        playerSpawnFX.SetActive(false);

        playerTank = Instantiate(samples[1], new Vector2(0, 0), Quaternion.identity);
        playerTank.SetActive(false);
    }

    public void GetPlayerFX(Vector2 position)
    {
        playerSpawnFX.transform.position = position;
        playerSpawnFX.SetActive(true);
    }

    public void SpawnPlayer(Vector2 position)
    {
        playerTank.transform.position = position;
        playerTank.SetActive(true);
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
        // gameObj.transform.rotation = Quaternion.Euler(0, 0, 180);
        gameObj.SetActive(true);

        enemyIndex++;
    }

    public void DestroyActiveTank()
    {
        foreach (GameObject gameObj in enemyList)
        {
            if (gameObj.activeSelf)
            {
                gameObj.SetActive(false);
            }
        }
    }

    public void FreezeActiveTank()
    {
        foreach (GameObject gameObj in enemyList)
        {
            if (gameObj.activeSelf)
            {
                gameObj.GetComponent<Enemy>().ActiveFreezing();
            }
        }
    }
}
