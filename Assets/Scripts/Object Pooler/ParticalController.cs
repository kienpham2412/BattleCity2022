using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalController : ObjectPooler
{
    public enum Partical
    {
        Collision = 0, Destroy = 1
    }
    private List<GameObject> collideExplosion, destroyExplosion;
    // Start is called before the first frame update
    protected override void Start()
    {
        collideExplosion = new List<GameObject>();
        destroyExplosion = new List<GameObject>();

        CreatePool();
    }

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

    public GameObject GetClone(Vector2 position, Quaternion rotation, Partical type)
    {
        if (type == Partical.Collision)
        {
            foreach (GameObject gameObj in collideExplosion)
            {
                if (!gameObj.activeSelf)
                {
                    gameObj.transform.position = position;
                    gameObj.transform.rotation = rotation;
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
                    gameObj.transform.rotation = rotation;
                    gameObj.SetActive(true);

                    return gameObj;
                }
            }
        }
        return null;
    }
}
