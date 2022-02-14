using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehav : MonoBehaviour
{
    protected bool firstTime = true;
    private void OnDisable()
    {
        if (firstTime)
        {
            firstTime = false;
            return;
        }
        
        Referee.singleton.SpawnItem();
    }
}
