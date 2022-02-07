using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBullet : BulletBehav
{
    // Start is called before the first frame update
    protected override void Start()
    {
        damage = 3;
        powerUp = true;
        base.Start();
    }
}
