using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Block
{
    private Collider2D thisCollider;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        thisCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "RegularBullet")
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), thisCollider, true);
        }
    }
}
