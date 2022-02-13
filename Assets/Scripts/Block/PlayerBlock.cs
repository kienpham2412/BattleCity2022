using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : Block
{
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
    }

    protected override void TakeDamage(object[] message)
    {
        if(!(bool) message[2]){
            base.TakeDamage(message);
        }
    }
}
