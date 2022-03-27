using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance;
    public GameState firstState;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Active();
    }

    public void Active()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        transform.GetChild(0).gameObject.SetActive(true);
        firstState.Perform();
    }
}
