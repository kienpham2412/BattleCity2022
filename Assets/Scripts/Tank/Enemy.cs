using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Tank
{
    // Start is called before the first frame update
    void Start()
    {
        playerOrigin = false;
        powerUp = false;
    }

    private void AutoShoot()
    {
        base.Shoot(playerOrigin);
    }

    private void OnEnable()
    {
        InvokeRepeating("AutoShoot", 1, 1);
    }

    protected override void OnDisable()
    {
        CancelInvoke();
        base.OnDisable();
    }

}
