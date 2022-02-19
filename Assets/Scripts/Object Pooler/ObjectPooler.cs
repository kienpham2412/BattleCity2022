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

    /// <summary>
    /// Create a pool
    /// </summary>
    /// <param name="sample">The sample object</param>
    /// <param name="amount">The amount of object to be cloned</param>
    protected virtual void CreatePool(GameObject sample, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Clone(sample, pool);
        }
    }

    /// <summary>
    /// Clone an object and put it in a pool
    /// </summary>
    /// <param name="sample">The sample object to be instantiated</param>
    /// <param name="samplePool">The list of objects which is the sample is added</param>
    protected virtual void Clone(GameObject sample, List<GameObject> samplePool)
    {
        GameObject gameObj = Instantiate(sample, poolObj.transform.position, Quaternion.identity, poolObj.transform);
        samplePool.Add(gameObj);
        gameObj.SetActive(false);
    }

    /// <summary>
    /// Get a cloned sample from pool and make it active
    /// </summary>
    /// <param name="position">The position of cloned sample</param>
    /// <param name="rotation">The rotation of cloned sample</param>
    /// <returns></returns>
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
