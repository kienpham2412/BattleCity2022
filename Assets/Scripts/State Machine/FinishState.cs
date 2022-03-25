using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishState : GameState
{
    public override void Perform()
    {
        gameObject.SetActive(true);
        StartCoroutine(Delay());
        Debug.Log("finish state triggered");
    }

    public override void Next()
    {
        gameObject.SetActive(false);
        GameStateController.Instance.Active();
        Debug.Log("finish");
    }

    IEnumerator Delay(){
        yield return new WaitForSeconds(3);
        Next();
    }
}
