using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLife : MonoBehaviour, ISubscriber
{
    public TMP_Text lifeDisplay;

    public void Handle(Message message)
    {
        lifeDisplay.text = PlayerBlock.Instance.playerLife.ToString();
    }

    void OnEnable()
    {
        MessageManager.Instance.AddSubscriber(MessageType.OnPlayerDestroyed, this);
    }
}
