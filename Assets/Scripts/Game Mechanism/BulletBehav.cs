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
    [Header("Damage attribute")]
    public int damage;
    public bool powerUp;

    protected Rigidbody2D rb;
    private static float speed = 20f;
    [HideInInspector] public bool playerOrigin;
    [HideInInspector] public int tankID;

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

    /// <summary>
    /// Create a damage to the collided block
    /// </summary>
    /// <param name="other">The object that can receive damage</param>
    private void CreatDamage(Collision2D other)
    {
        other.gameObject.GetComponent<IBlock>().TakeDamage(new DamageAttribute(damage, powerUp, playerOrigin));
        ParticalController.Instance.GetClone(other.contacts[0].point, Partical.Collision);
    }

    /// <summary>
    /// Set the tank origin that shoot this bullet
    /// </summary>
    /// <param name="isPlayer">True if the bullet is shoot by the player</param>
    /// <param name="tankID">the instance ID of the game object</param>
    public void SetOrigin(bool isPlayer, int tankID)
    {
        playerOrigin = isPlayer;
        this.tankID = tankID;
    }
}