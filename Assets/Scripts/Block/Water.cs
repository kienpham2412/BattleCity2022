using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private Collider2D cl;
    // // Start is called before the first frame update
    void Start()
    {
        cl = GetComponent<Collider2D>();
        // health = 4;
    }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "RegularBullet")
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), cl, true);
            Debug.Log($"collide with: {other.gameObject.tag}");
        }
    }
}
