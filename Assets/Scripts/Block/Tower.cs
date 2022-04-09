using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : Block
{
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
    }

    protected override void TakeDamage(object[] message)
    {
        // if the bullet is shoot by the enemy
        if (!(bool)message[2])
        {
            base.TakeDamage(message);
        }
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == "PlayGame")
            MessageManager.Instance.SendMessage(new Message(MessageType.OnGameOver));
    }
}
