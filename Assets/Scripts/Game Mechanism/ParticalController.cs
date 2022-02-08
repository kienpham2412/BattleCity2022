using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalController : ObjectPooler
{
    public static ParticalController singleton;
    // Start is called before the first frame update
    protected override void Start()
    {
        singleton = GetComponent<ParticalController>();
        base.Start();
    }
}
