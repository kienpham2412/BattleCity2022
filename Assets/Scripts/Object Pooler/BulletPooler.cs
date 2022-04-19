using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : ObjectPooler
{
    public static BulletPooler singleton;
    private List<GameObject> regularList, powerList;

    // Start is called before the first frame update
    protected override void Start()
    {
        regularList = new List<GameObject>();
        powerList = new List<GameObject>();
        singleton = GetComponent<BulletPooler>();

        CreatePool();
    }

    /// <summary>
    /// Create a pool for the bullets
    /// </summary>
    private void CreatePool()
    {
        for (int i = 0; i < 10; i++)
        {
            Clone(samples[0], regularList);
            Clone(samples[1], powerList);
        }
    }

    /// <summary>
    /// Get a bullet
    /// </summary>
    /// <param name="position">Position to spawn bullet</param>
    /// <param name="rotation">Direction of the bullet</param>
    /// <param name="powerUp">Is the bullet power up</param>
    /// <param name="tankID">The instance ID of the thank that spawn this bullet</param>
    /// <param name="playerOrigin">Is the bullet shooted by the player?</param>
    /// <returns></returns>
    public GameObject GetClone(Vector2 position, Quaternion rotation, bool powerUp, int tankID, bool playerOrigin = true)
    {
        if (powerUp)
        {
            foreach (GameObject bullet in powerList)
            {
                if (!bullet.activeSelf)
                {
                    bullet.transform.position = position;
                    bullet.transform.rotation = rotation;
                    bullet.GetComponent<BulletBehav>().SetOrigin(playerOrigin, tankID);
                    bullet.SetActive(true);
                    return bullet;
                }
            }
        }
        else
        {
            foreach (GameObject bullet in regularList)
            {
                if (!bullet.activeSelf)
                {
                    bullet.transform.position = position;
                    bullet.transform.rotation = rotation;
                    bullet.GetComponent<BulletBehav>().SetOrigin(playerOrigin, tankID);
                    bullet.SetActive(true);
                    return bullet;
                }
            }
        }

        return null;
    }
}
