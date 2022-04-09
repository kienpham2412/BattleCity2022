using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : Block
{
    private bool isDisabledByDefeat;
    protected override void TakeDamage(object[] message)
    {
        // if the bullet is shoot by the enemy
        if (!(bool)message[2])
        {
            // base.TakeDamage(message);
            if (immotal) return;

            health -= (int)message[0];
            if (health <= 0)
            {
                isDisabledByDefeat = true;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        health = 5;
        isDisabledByDefeat = false;
    }

    private void OnDisable()
    {
        if (isDisabledByDefeat)
            MessageManager.Instance.SendMessage(new Message(MessageType.OnGameOver));
    }
}
