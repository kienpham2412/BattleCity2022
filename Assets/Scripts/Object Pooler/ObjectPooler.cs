using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPooler : MonoBehaviour
{
    public GameObject poolObj;
    public GameObject[] samples;
    private List<GameObject> pool;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        pool = new List<GameObject>();
        CreatePool(samples[0], 20);
    }

    protected virtual void CreatePool(GameObject gameObj, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Clone(gameObj, pool);
        }
    }

    protected virtual void Clone(GameObject sample, List<GameObject> samplePool)
    {
        GameObject gameObj = Instantiate(sample, poolObj.transform.position, Quaternion.identity, poolObj.transform);
        samplePool.Add(gameObj);
        gameObj.SetActive(false);
    }

    public virtual GameObject GetClone(Vector2 position, Quaternion rotation)
    {
        foreach (GameObject gameObj in pool)
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
}
