using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPooler : ObjectPooler
{
    private List<GameObject> itemPool;
    // Start is called before the first frame update
    protected override void Start()
    {
        itemPool = new List<GameObject>();

        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < samples.Length; i++)
        {
            Clone(samples[i], itemPool);
        }
    }

    public GameObject GetClone(Vector2 position)
    {
        itemPool.Shuffle();
        GameObject gameObj = itemPool[0];
        gameObj.transform.position = position;
        gameObj.SetActive(true);

        return gameObj;
    }
}
