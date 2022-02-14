using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFX : AnimController
{
    protected bool firstTime = true;
    // Start is called before the first frame update
    protected override void Awake()
    {
        length = 1f;
    }

    protected virtual void OnDisable()
    {
        if (firstTime)
        {
            firstTime = false;
            return;
        }

        Referee.singleton.GetEnemyClone(gameObject.transform.position);
    }
}
