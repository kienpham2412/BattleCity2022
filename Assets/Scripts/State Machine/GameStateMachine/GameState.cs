using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameState nextState;

    public virtual void Perform()
    {

    }

    public virtual void Next()
    {
        nextState.Perform();
    }
}
