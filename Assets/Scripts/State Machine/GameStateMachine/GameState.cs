using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public GameState nextState;

    /// <summary>
    /// Handle the performance of a state
    /// </summary>
    public virtual void Perform()
    {

    }

    /// <summary>
    /// Handle the exit of a state
    /// </summary>
    public virtual void Next()
    {
        nextState.Perform();
    }
}
