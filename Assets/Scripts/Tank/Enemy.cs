using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Tank
{
    // Start is called before the first frame update
    protected override void Start()
    {
        playerOrigin = false;
        powerUp = false;

        base.Start();
        TurnDown();
    }

    public void ActiveFreezing()
    {
        StartCoroutine(Freeze());
    }

    private void AutoShoot()
    {
        base.Shoot(playerOrigin);
    }

    IEnumerator Freeze()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(10f);
        rb.constraints = RigidbodyConstraints2D.None;
    }

    // private void OnEnable()
    // {
    //     InvokeRepeating("AutoShoot", 1, 1);
    // }

    protected override void OnDisable()
    {
        // CancelInvoke();
        base.OnDisable();
    }
}
