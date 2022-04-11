using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFX : AnimController
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        length = 1f;
    }

    protected override IEnumerator Deactive()
    {
        yield return new WaitForSeconds(length);
        Destroy(gameObject);
    }

    protected virtual void OnDisable()
    {
        TankSpawner.Instance.GetEnemyClone(gameObject.transform.position);
    }
}
