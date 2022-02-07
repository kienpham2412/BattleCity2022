using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    protected int health;

    protected virtual void TakeDamage(object[] message)
    {
        health -= (int)message[0];
        if(health <=0){
            gameObject.SetActive(false);
        }
        // Debug.Log($"Hit by bullet at {gameObject.transform.position} with {damage} damage");
    }
}
