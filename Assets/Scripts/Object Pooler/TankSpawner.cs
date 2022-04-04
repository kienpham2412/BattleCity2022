using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : ObjectPooler, ISubscriber
{
    public static TankSpawner Instance;
    private List<GameObject> enemyList, spawnFXList;
    private GameObject playerSpawnFX, playerTank;
    private int enemyIndex = 0;
    private int destroyedTank = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        Instance = this;
        enemyList = new List<GameObject>();
        spawnFXList = new List<GameObject>();
        MessageManager.Instance.AddSubscriber(MessageType.OnEnemyDestroyed, this);
        MessageManager.Instance.AddSubscriber(MessageType.OnGameRestart, this);

        CreatePlayer();
        CreatePool();
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case MessageType.OnEnemyDestroyed:
                if (CheckRemainingEnemy())
                    GetClone((Vector3)message.content, Quaternion.identity);
                CountDestroyed();
                break;
            case MessageType.OnGameRestart:
                enemyIndex = 0;
                destroyedTank = 0;
                break;
        }
    }

    private void CreatePool()
    {
        for (int i = 0; i < 20; i++)
        {
            if (i <= 4)
            {
                Clone(samples[2], spawnFXList);
            }

            int randomIndex = Random.Range(3, 5);
            Clone(samples[randomIndex], enemyList);
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
        if (enemyIndex < enemyList.Count)
        {
            GameObject gameObj = enemyList[enemyIndex];
            gameObj.transform.position = position;
            gameObj.SetActive(true);

            enemyIndex++;
        }
    }

    public bool CheckRemainingEnemy()
    {
        if (enemyIndex < 20)
        {
            return true;
        }
        return false;
    }

    public void CountDestroyed()
    {
        destroyedTank++;
        Debug.Log(destroyedTank);
        if (destroyedTank == 20)
        {
            MessageManager.Instance.SendMessage(new Message(MessageType.OnGameFinish));
        }
    }
}
