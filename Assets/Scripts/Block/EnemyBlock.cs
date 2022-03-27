using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlock : Block
{
    [SerializeField] public int enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        health = enemyHealth;
    }
}
