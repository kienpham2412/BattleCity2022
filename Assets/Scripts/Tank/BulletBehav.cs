using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehav : MonoBehaviour
{
    private Rigidbody2D rb;
    private static float raycastLength;
    public float speed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        raycastLength = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        Vector3 raycastStartPos = gameObject.transform.position + gameObject.transform.up * 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(raycastStartPos, gameObject.transform.up, raycastLength);

        if (hit.collider != null)
        {
            // Debug.Log("hit something");
            gameObject.SetActive(false);
        }

        // Debug.DrawRay(raycastStartPos, gameObject.transform.up * raycastLength, Color.red);
    }
}