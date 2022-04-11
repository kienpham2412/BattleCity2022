using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : Block
{
    private bool isDisabledByDefeat;

    public override void TakeDamage(DamageAttribute damageAttribute)
    {
        // if the bullet is shoot by the enemy
        if (!damageAttribute.playerOrigin)
        {
            // base.TakeDamage(message);
            if (immotal) return;

            health -= damageAttribute.damage;
            if (health <= 0)
            {
                isDisabledByDefeat = true;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        health = 10;
        isDisabledByDefeat = false;
    }

    private void OnDisable()
    {
        if (isDisabledByDefeat)
            MessageManager.Instance.SendMessage(new Message(MessageType.OnGameOver));
    }
}
