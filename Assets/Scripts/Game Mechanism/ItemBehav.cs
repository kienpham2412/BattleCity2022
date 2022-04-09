using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehav : MonoBehaviour, ISubscriber
{
    private enum Item
    {
        Helmet = 0, Shovel = 1, Grenade = 2, Clock = 3, Star = 4, Tank = 5
    }

    [Space]
    [SerializeField]
    private Item itemType;
    protected bool firstTime = true;

    private void Start()
    {
        MessageManager.Instance.AddSubscriber(MessageType.OnGameFinish, this);
    }

    private void TriggerEvent()
    {
        switch (itemType)
        {
            case Item.Star:
                Player.Instance.TriggerPowerUp();
                break;
            case Item.Clock:
                MessageManager.Instance.SendMessage(new Message(MessageType.OnClockAcquired));
                break;
            case Item.Shovel:
                Debug.Log("Shovel");
                break;
            case Item.Grenade:
                MessageManager.Instance.SendMessage(new Message(MessageType.OnGrenadeAcquired));
                break;
            case Item.Tank:
                Debug.Log("Tank");
                break;
            default:
                break;
        }
    }

    public void Handle(Message message)
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (firstTime)
        {
            firstTime = false;
            return;
        }
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
