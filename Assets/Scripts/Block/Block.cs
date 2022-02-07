using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    public Coordinate position;
    protected int health;

    /// <summary>
    /// This method is invoked when the block is hit by the bullet
    /// </summary>
    /// <param name="message">The bullet infomation</param>
    protected virtual void TakeDamage(object[] message)
    {
        health -= (int)message[0];
        if(health <=0){
            gameObject.SetActive(false);
        }
        // Debug.Log($"Hit by bullet at {gameObject.transform.position} with {damage} damage");
    }
}
