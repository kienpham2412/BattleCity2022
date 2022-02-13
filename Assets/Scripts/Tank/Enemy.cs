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
        InvokeRepeating("AutoShoot", 1, 1);
    }

    private void AutoShoot(){
        base.Shoot(playerOrigin);
    }

    private void OnDisable() {
        CancelInvoke();
    }

}
