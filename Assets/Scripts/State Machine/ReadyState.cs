using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyState : GameState
{
    public override void Perform(){
        Debug.Log("ready state triggered");
        StartCoroutine(Intro());
    }

    public override void Next()
    {
        gameObject.SetActive(false);
        Debug.Log("finish ready state");
        base.Next();
    }

    IEnumerator Intro(){
        yield return new WaitForSeconds(2);
        Next();
    }
}
