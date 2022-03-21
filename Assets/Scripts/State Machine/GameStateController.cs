using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public GameState firstState;

    // Start is called before the first frame update
    void Start()
    {
        firstState.Perform();
    }
}
