using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : GameState, ISubscriber
{


    public void Handle(Message message)
    {
        switch (message.type)
        {
            case MessageType.OnGameFinish:
                Next();
                break;
            case MessageType.OnGameOver:
                gameObject.SetActive(false);
                GameStateController.Instance.TriggerGameOverState();
                break;
        }
    }

    public override void Perform()
    {
        Referee.Instance.LoadPlayMap();
        Referee.Instance.SpawnTanks();
        gameObject.SetActive(true);
    }

    public override void Next()
    {
        gameObject.SetActive(false);
        base.Next();
    }

    private void OnEnable()
    {
        MessageManager.Instance.AddSubscriber(MessageType.OnGameFinish, this);
        MessageManager.Instance.AddSubscriber(MessageType.OnGameOver, this);
    }
}
