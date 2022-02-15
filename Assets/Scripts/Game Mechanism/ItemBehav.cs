using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehav : MonoBehaviour
{
    private enum Item
    {
        Helmet = 0, Shovel = 1, Grenade = 2, Clock = 3, Star = 4, Tank = 5
    }

    [SerializeField]
    private Item thisItem;
    protected bool firstTime = true;

    private void TriggerEvent()
    {
        switch (thisItem)
        {
            case Item.Clock:
                Referee.singleton.FreezeEnemies();
                break;
            case Item.Shovel:
                Debug.Log("Shovel");
                break;
            case Item.Grenade:
                Referee.singleton.DestroyEnemies();
                break;
            case Item.Tank:
                Debug.Log("Tank");
                break;
            default:
                break;
        }
    }

    private void OnDisable()
    {
        if (firstTime)
        {
            firstTime = false;
            return;
        }

        Referee.singleton.SpawnItem();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerTank"))
        {
            TriggerEvent();
            gameObject.SetActive(false);
        }
    }
}
