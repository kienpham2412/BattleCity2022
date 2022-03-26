using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : GameState, ISubscriber
{
    private void Start()
    {
        MessageManager.Instance.AddSubscriber(MessageType.OnGameFinish, this);
    }

    public void Handle(Message message){
        Next();
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
