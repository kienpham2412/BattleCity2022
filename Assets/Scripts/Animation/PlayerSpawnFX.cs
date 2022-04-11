using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnFX : SpawnFX
{
    protected override void OnDisable()
    {
        TankSpawner.Instance.SpawnPlayer(gameObject.transform.position);
    }
}
