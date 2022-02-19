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

    /// <summary>
    ///  Create a pool of spawn effects and the enemy tanks
    /// </summary>
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

    /// <summary>
    /// Create the player clone
    /// </summary>
    private void CreatePlayer()
    {
        playerSpawnFX = Instantiate(samples[0], new Vector2(0, 0), Quaternion.identity);
        playerSpawnFX.SetActive(false);

        playerTank = Instantiate(samples[1], new Vector2(0, 0), Quaternion.identity);
        playerTank.SetActive(false);
    }

    /// <summary>
    /// Get the player spawn effect object
    /// </summary>
    /// <param name="position">The position where the effect is placed</param>
    public void GetPlayerFX(Vector2 position)
    {
        playerSpawnFX.transform.position = position;
        playerSpawnFX.SetActive(true);
    }

    /// <summary>
    /// Get the player tank
    /// </summary>
    /// <param name="position">The position where the tank is placed</param>
    public void SpawnPlayer(Vector2 position)
    {
        playerTank.transform.position = position;
        playerTank.SetActive(true);
    }

    /// <summary>
    /// Get a the cloned enemy spawn effect
    /// </summary>
    /// <param name="position">The position where the effect is placed</param>
    /// <param name="rotation">The rotation of this object</param>
    /// <returns></returns>
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

    /// <summary>
    /// Get a cloned enemy from the pool
    /// </summary>
    /// <param name="position">The position where the enemy tank is placed</param>
    public void GetEnemyClone(Vector2 position)
    {
        GameObject gameObj = enemyList[enemyIndex];
        gameObj.transform.position = position;
        // gameObj.transform.rotation = Quaternion.Euler(0, 0, 180);
        gameObj.SetActive(true);

        enemyIndex++;
    }

    /// <summary>
    /// Destroy all active tanks on the map
    /// </summary>
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

    /// <summary>
    /// Freeze all active tanks on the map
    /// </summary>
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
