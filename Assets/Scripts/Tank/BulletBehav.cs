using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehav : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D cl;
    private static float raycastLength;
    public float speed = 200f;
    private int mask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<Collider2D>();
        raycastLength = 0.2f;
        mask = LayerMask.GetMask("MyWater");
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "MyWater")
        {
            gameObject.SetActive(false);
        }
        else
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), cl);
        }
    }

    void FixedUpdate()
    {
        Vector3 raycastStartPos = gameObject.transform.position + gameObject.transform.up * 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(raycastStartPos, gameObject.transform.up, raycastLength);

        if (hit.collider != null)
        {
            // Debug.Log("hit something");
            if (hit.collider.tag != "MyWater")
            {
                gameObject.SetActive(false);
            }
            if (hit.collider.tag != "Border")
            {
                hit.collider.gameObject.SetActive(false);
            }
        }

        // Debug.DrawRay(raycastStartPos, gameObject.transform.up * raycastLength, Color.red);
    }
}