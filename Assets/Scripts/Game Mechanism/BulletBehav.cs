using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehav : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D cl;
    // private static float raycastLength;
    private static float speed = 20f;
    private int mask;
    protected int damage;
    protected bool powerUp;
    private object[] message;
    public bool playerOrigin;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<Collider2D>();
        mask = LayerMask.GetMask("MyWater");
        // raycastLength = 0.2f;

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
        if (other.gameObject.tag == "MyWater")
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), cl);
            return;
        }

        if (other.gameObject.tag != "Border" && other.gameObject.tag != "RegularBullet"){
            other.transform.SendMessage("TakeDamage", message);
        }

        gameObject.SetActive(false);
        Debug.Log($"collide with: {other.gameObject.tag}");
    }

    // void FixedUpdate()
    // {
    //     Vector3 raycastStartPos = gameObject.transform.position + gameObject.transform.up * 0.5f;
    //     RaycastHit2D hit = Physics2D.Raycast(raycastStartPos, gameObject.transform.up, raycastLength);

    //     if (hit.collider == null)
    //     {
    //         return;
    //     }

    //     if (hit.collider.tag != "MyWater")
    //     {
    //         gameObject.SetActive(false);
    //         // Referee.singleton.SpawnClExplosion(hit.point);
    //     }
        
    //     if (hit.collider.tag != "Border" && hit.collider.tag != "RegularBullet")
    //     {
    //         // hit.collider.gameObject.SetActive(false);
    //         hit.transform.SendMessage("TakeDamage", message);
    //     }


    //     // Debug.DrawRay(raycastStartPos, gameObject.transform.up * raycastLength, Color.red);
    // }

    public void setPlayerOrigin(bool isPlayer)
    {
        playerOrigin = isPlayer;
    }
}