using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlock
{
    /// <summary>
    /// Handle the event when an object get damage from bullet
    /// </summary>
    /// <param name="damageAttribute">Information of the damage</param>
    public void TakeDamage(DamageAttribute damageAttribute);
}
