using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IBlock
{
    public int health;
    public bool immotal = false;

    /// <summary>
    /// This method is invoked when the block is hit by the bullet
    /// </summary>
    /// <param name="message">The bullet infomation</param>
    public virtual void TakeDamage(DamageAttribute damageAttribute)
    {
        if (immotal) return;

        health -= damageAttribute.damage;
        if (health <= 0) Destroy(gameObject);
    }
}
