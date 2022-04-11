using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Block : MonoBehaviour, IBlock
{
    protected int health;
    protected bool immotal = false;

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
