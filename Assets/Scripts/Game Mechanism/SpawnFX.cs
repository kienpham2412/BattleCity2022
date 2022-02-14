using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFX : AnimController
{
    private bool firstTime = true;
    // Start is called before the first frame update
    protected override void Awake()
    {
        length = 1f;
    }

    private void OnDisable()
    {
        if (firstTime)
        {
            firstTime = false;
            return;
        }

        EnemySpawner.singleton.GetEnemyClone(gameObject.transform.position);
    }
}
