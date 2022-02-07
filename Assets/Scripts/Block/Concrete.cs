using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concrete : Block
{
    // Start is called before the first frame update
    void Start()
    {
        health = 10;
    }

    protected override void TakeDamage(object[] message)
    {
        if ((bool) message[1])
        {
            base.TakeDamage(message);
        }
    }
}
