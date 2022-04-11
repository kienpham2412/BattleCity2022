using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : Block
{
    public static PlayerBlock Instance;
    [SerializeField] private GameObject shieldFX;
    public int playerHealth;
    private float immotalTime;

    private void SetImmmotal(bool isImmotal)
    {
        immotal = isImmotal;
        shieldFX.SetActive(isImmotal);
    }

    public override void TakeDamage(DamageAttribute damageAttribute)
    {
        if (immotal)
        {
            return;
        }

        health -= damageAttribute.damage;

        if (health <= 0)
        {
            Referee.Instance.playerLife--;
            MessageManager.Instance.SendMessage(new Message(MessageType.OnPlayerDestroyed));

            if (Referee.Instance.playerLife < 0)
                MessageManager.Instance.SendMessage(new Message(MessageType.OnGameOver));
                
            Destroy(gameObject);
        }
    }

    public void ResetHealth()
    {
        health = playerHealth;
    }

    IEnumerator ImmotalActive()
    {
        immotalTime = 10f;
        SetImmmotal(true);
        yield return new WaitForSeconds(immotalTime);
        SetImmmotal(false);
    }

    public void Immotal()
    {
        StartCoroutine(ImmotalActive());
    }

    private void OnEnable()
    {
        Instance = this;

        immotalTime = 5f;
        ResetHealth();
        StartCoroutine(ImmotalActive());
    }
}
