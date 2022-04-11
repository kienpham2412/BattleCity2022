using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAttribute
{
    public DamageAttribute(int damage, bool powerUp, bool playerOrigin)
    {
        this.damage = damage;
        this.powerUp = powerUp;
        this.playerOrigin = playerOrigin;
    }
    public int damage;
    public bool powerUp;
    public bool playerOrigin;
}

public class BulletBehav : MonoBehaviour
{
    protected Rigidbody2D rb;
    private static float speed = 20f;
    protected int damage;
    protected bool powerUp;
    public bool playerOrigin;
    public int tankID;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    private void CreatDamage(Collision2D other)
    {
        other.gameObject.GetComponent<IBlock>().TakeDamage(new DamageAttribute(damage, powerUp, playerOrigin));
        ParticalController.Instance.GetClone(other.contacts[0].point, Partical.Collision);
    }

    public void setOrigin(bool isPlayer, int tankID)
    {
        playerOrigin = isPlayer;
        this.tankID = tankID;
    }
}