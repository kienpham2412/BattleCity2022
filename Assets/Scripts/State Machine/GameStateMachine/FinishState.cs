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
        if (Referee.Instance.CheckIfTheGameEnd()) nextState.Perform();
        else GameStateController.Instance.Active();

        Debug.Log("finish");
    }

    /// <summary>
    /// Delay to the next state
    /// </summary>
    /// <returns></returns>
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        Next();
    }
}
