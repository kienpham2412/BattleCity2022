using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    Helmet = 0, Shovel = 1, Grenade = 2, Clock = 3, Star = 4, Tank = 5
}

public class ItemBehav : MonoBehaviour, ISubscriber
{
    [SerializeField]
    public Item itemType;
    protected Animator animator;
    private IEnumerator countDown;

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
                Referee.Instance.StartFreezing();
                break;
            case Item.Shovel:
                Debug.Log("Shovel");
                break;
            case Item.Grenade:
                foreach (EnemyAI enemy in FindObjectsOfType<EnemyAI>())
                    enemy.TakeDamage(new DamageAttribute(100, false, true));
                break;
            case Item.Tank:
                Referee.Instance.playerLife++;
                PlayerLife.Instance.lifeDisplay.text = Referee.Instance.playerLife.ToString();
                break;
            case Item.Helmet:
                Player.Instance.Immotal();
                break;
            default:
                break;
        }
    }

    public void Handle(Message message)
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerTank"))
        {
            TriggerEvent();
            Referee.Instance.SpawnItem();
            gameObject.SetActive(false);
        }
    }

    protected IEnumerator Deactive()
    {
        yield return new WaitForSeconds(10f);
        animator.SetBool("isFading", true);
        yield return new WaitForSeconds(5f);
        Referee.Instance.SpawnItem();
        gameObject.SetActive(false);
    }

    protected void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isFading", false);

        if (countDown != null)
            StopCoroutine(countDown);

        countDown = Deactive();
        StartCoroutine(countDown);
    }

    protected void OnDisable()
    {
        if (countDown != null)
            StopCoroutine(countDown);
    }
}
