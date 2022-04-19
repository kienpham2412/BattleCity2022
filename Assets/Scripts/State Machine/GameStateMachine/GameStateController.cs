using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance;
    public GameState firstState;
    public GameState gameOverState;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Active();
    }

    /// <summary>
    /// Start the game state machine
    /// </summary>
    public void Active()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        transform.GetChild(0).gameObject.SetActive(true);
        firstState.Perform();
    }

    /// <summary>
    /// Trigger the game over state
    /// </summary>
    public void TriggerGameOverState(){
        gameOverState.Perform();
    }
}
