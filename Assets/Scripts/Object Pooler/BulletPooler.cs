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
    /// Create a bullet pool
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
    /// Get a bullet clone
    /// </summary>
    /// <param name="position">The position where the bullet is placed</param>
    /// <param name="rotation">The rotation of the bullet</param>
    /// <param name="powerUp">Power up state of the bullet</param>
    /// <param name="tankID">ID object the the tank that shoot this bullet</param>
    /// <param name="playerOrigin">Is that the player or enemy tank</param>
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
                    bullet.GetComponent<PowerUpBullet>().setOrigin(playerOrigin, tankID);
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
                    bullet.GetComponent<RegularBullet>().setOrigin(playerOrigin, tankID);
                    bullet.SetActive(true);
                    return bullet;
                }
            }
        }

        return null;
    }
}
