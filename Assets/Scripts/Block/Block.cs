using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    protected int health;
    protected bool immotal = false;

    /// <summary>
    /// This method is invoked when the block is hit by the bullet
    /// </summary>
    /// <param name="message">The bullet infomation</param>
    protected virtual void TakeDamage(object[] message)
    {
        if (immotal) return;

        health -= (int)message[0];
        if (health <= 0) gameObject.SetActive(false);
    }
}
