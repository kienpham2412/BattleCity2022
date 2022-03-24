using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : GameState
{
    public static PlayState Instance;

    private void Start()
    {
        Instance = this;
    }

    public override void Perform()
    {
        Referee.Instance.LoadPlayMap();            
        Referee.Instance.SpawnTanks();
        gameObject.SetActive(true);
        Debug.Log("play state triggered");
    }

    public override void Next()
    {
        gameObject.SetActive(false);
        Debug.Log("finish play state");
        base.Next();
    }
}
