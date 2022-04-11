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

    public override void TakeDamage(DamageAttribute damageAttribute){
        if (damageAttribute.powerUp)
        {
            base.TakeDamage(damageAttribute);
        }
    }
}
