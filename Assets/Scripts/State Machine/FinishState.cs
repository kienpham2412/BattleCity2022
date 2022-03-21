using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishState : GameState
{
    public override void Perform()
    {
        gameObject.SetActive(true);
        Debug.Log("finish state triggered");
    }

    public override void Next()
    {
        Debug.Log("finish");
    }
}
