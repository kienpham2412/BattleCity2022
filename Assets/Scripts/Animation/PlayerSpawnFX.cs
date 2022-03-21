using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnFX : SpawnFX
{
    protected override void OnDisable()
    {
        if (firstTime)
        {
            firstTime = false;
            return;
        }

        TankSpawner.Instance.SpawnPlayer(gameObject.transform.position);
    }
}
