using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : Block
{
    public static PlayerBlock Instance;
    [SerializeField] private GameObject shieldFX;
    public int playerHealth;
    public int playerLife;
    private float immotalTime;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ResetHealth();
        playerLife = 3;
    }

    private void SetImmmotal(bool isImmotal)
    {
        immotal = isImmotal;
        shieldFX.SetActive(isImmotal);
    }

    protected override void TakeDamage(object[] message)
    {
        if (immotal)
        {
            return;
        }

        health -= (int)message[0];
        if (health <= 0)
        {
            playerLife--;
            gameObject.SetActive(false);
            MessageManager.Instance.SendMessage(new Message(MessageType.OnPlayerDestroyed));
        }
        if (playerLife < 0) MessageManager.Instance.SendMessage(new Message(MessageType.OnGameOver));
    }

    public void ResetHealth()
    {
        health = playerHealth;
    }

    IEnumerator ImmotalActive()
    {
        SetImmmotal(true);
        yield return new WaitForSeconds(immotalTime);
        SetImmmotal(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Helmet"))
        {
            immotalTime = 10f;
            StartCoroutine(ImmotalActive());
        }
    }

    private void OnEnable()
    {
        immotalTime = 5f;
        ResetHealth();
        StartCoroutine(ImmotalActive());
    }
}
