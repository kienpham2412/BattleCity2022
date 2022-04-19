using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour, ISubscriber
{
    [Header("Samples")]
    public GameObject playerSpawnFX;
    public GameObject enemySpawnFX;
    public GameObject playerTank;
    public GameObject[] enemyTanks;

    public static TankSpawner Instance;
    private const int SPAWN_LIMIT = 20;
    private int enemyIndex = 0;
    private int destroyedTank = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        MessageManager.Instance.AddSubscriber(MessageType.OnEnemyDestroyed, this);
        MessageManager.Instance.AddSubscriber(MessageType.OnGameRestart, this);
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case MessageType.OnEnemyDestroyed:
                if (CheckRemainingEnemy())
                    GetEnemyFX((Vector3)message.content);
                CountDestroyed();
                break;
            case MessageType.OnGameRestart:
                enemyIndex = 0;
                destroyedTank = 0;
                break;
        }
    }

    /// <summary>
    /// Get player spawn effect
    /// </summary>
    /// <param name="position">Position to spawn effect</param>
    public void GetPlayerFX(Vector2 position)
    {
        Instantiate(playerSpawnFX, position, Quaternion.identity);
    }

    /// <summary>
    /// Spawn player
    /// </summary>
    /// <param name="position">Position to spawn player</param>
    public void SpawnPlayer(Vector2 position)
    {
        Instantiate(playerTank, position, Quaternion.identity);
    }

    /// <summary>
    /// Get Enemy spawn effect
    /// </summary>
    /// <param name="position">Position to spawn effect</param>
    public void GetEnemyFX(Vector2 position)
    {
        Instantiate(enemySpawnFX, position, Quaternion.identity);
    }

    /// <summary>
    /// Spawn enemy
    /// </summary>
    /// <param name="position">Position to spawn enemy</param>
    public void GetEnemyClone(Vector2 position)
    {
        if (enemyIndex < SPAWN_LIMIT)
        {
            int random = Random.Range(0, enemyTanks.Length);
            GameObject gameObj = Instantiate(enemyTanks[random], position, Quaternion.identity);
            enemyIndex++;
        }
    }

    /// <summary>
    /// Check remaining enemy on the stage
    /// </summary>
    /// <returns>True if there are still enemies on the stage</returns>
    public bool CheckRemainingEnemy()
    {
        if (enemyIndex < SPAWN_LIMIT)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Calculate the amount of destroyed enemies
    /// </summary>
    public void CountDestroyed()
    {
        destroyedTank++;
        if (destroyedTank == SPAWN_LIMIT)
        {
            MessageManager.Instance.SendMessage(new Message(MessageType.OnGameFinish));
        }
    }
}
