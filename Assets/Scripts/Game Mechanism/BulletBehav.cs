using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehav : MonoBehaviour
{
    protected Rigidbody2D rb;
    private static float speed = 20f;
    protected int damage;
    protected bool powerUp;
    private object[] message;
    public bool playerOrigin;
    public int tankID;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        message = new object[3];
        message[0] = damage;
        message[1] = powerUp;
        message[2] = playerOrigin;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        string otherTag = other.gameObject.tag;
        int otherID = other.gameObject.GetInstanceID();

        if (otherTag == "MyWater")
        {
            return;
        }

        if (otherTag == "RegularBullet")
        {
            gameObject.SetActive(false);
            return;
        }

        if (otherID == tankID)
        {
            return;
        }

        if (otherTag == "EnemyTank" && otherID != tankID)
        {
            if (playerOrigin)
            {
                CreatDamage(other);
            }

            gameObject.SetActive(false);
            return;
        }

        CreatDamage(other);
        gameObject.SetActive(false);

        // Debug.Log($"collide with: {otherTag}");
    }

    private void CreatDamage(Collision2D other)
    {
        other.transform.SendMessage("TakeDamage", message);
        Referee.singleton.SpawnClExplosion(other.contacts[0].point);
    }

    public void setOrigin(bool isPlayer, int tankID)
    {
        playerOrigin = isPlayer;
        this.tankID = tankID;
    }
}