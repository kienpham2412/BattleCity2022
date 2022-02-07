using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBullet : BulletBehav
{
    // Start is called before the first frame update
    protected override void Start()
    {
        damage = 1;
        powerUp = false;
        base.Start();
    }
}
