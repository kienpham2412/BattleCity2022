using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Partical
{
    Collision = 0, Destroy = 1
}

public class ParticalController : ObjectPooler
{
    public AudioClip destroyingClip;
    public static ParticalController Instance;
    private List<GameObject> collideExplosion, destroyExplosion;

    // Start is called before the first frame update
    protected override void Start()
    {
        Instance = this;
        collideExplosion = new List<GameObject>();
        destroyExplosion = new List<GameObject>();

        CreatePool();
    }

    /// <summary>
    /// Create a partical pool
    /// </summary>
    private void CreatePool()
    {
        for (int i = 0; i < 20; i++)
        {
            Clone(samples[0], collideExplosion);

            if (i < 5)
            {
                Clone(samples[1], destroyExplosion);
            }
        }
    }

    /// <summary>
    /// Get a partical clone
    /// </summary>
    /// <param name="position">The position where the partical is placed</param>
    /// <param name="type">The type of partical</param>
    /// <returns></returns>
    public GameObject GetClone(Vector2 position, Partical type)
    {
        if (type == Partical.Collision)
        {
            foreach (GameObject gameObj in collideExplosion)
            {
                if (!gameObj.activeSelf)
                {
                    gameObj.transform.position = position;
                    gameObj.transform.rotation = Quaternion.identity;
                    gameObj.SetActive(true);

                    return gameObj;
                }
            }
        }
        else if (type == Partical.Destroy)
        {
            foreach (GameObject gameObj in destroyExplosion)
            {
                if (!gameObj.activeSelf)
                {
                    gameObj.transform.position = position;
                    gameObj.transform.rotation = Quaternion.identity;
                    gameObj.SetActive(true);
                    AudioController.Instance.PlayUsingReserveSource(destroyingClip);

                    return gameObj;
                }
            }
        }
        return null;
    }
}
