using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{
    public GameObject shootingPos;
    public Animator tankAnimator;
    private static Vector3 moveForward;
    private static Quaternion lookUp, lookDown, lookLeft, lookRight;
    private Rigidbody2D rb;
    private float speed = 1f;

    // // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        lookUp = Quaternion.Euler(0, 0, 0);
        lookDown = Quaternion.Euler(0, 0, 180);
        lookLeft = Quaternion.Euler(0, 0, 90);
        lookRight = Quaternion.Euler(0, 0, -90);
    }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    /// <summary>
    /// Turn the tank up
    /// </summary>
    protected void TurnUp()
    {
        gameObject.transform.rotation = lookUp;
        moveForward = gameObject.transform.up * speed;
        Debug.Log("Turn up");
    }

    /// <summary>
    /// Turn the tank down
    /// </summary>
    protected void TurnDown()
    {
        gameObject.transform.rotation = lookDown;
        moveForward = gameObject.transform.up * speed;
        Debug.Log("Turn down");
    }

    /// <summary>
    /// Turn the tank left
    /// </summary>
    protected void TurnLeft()
    {
        gameObject.transform.rotation = lookLeft;
        moveForward = gameObject.transform.up * speed;
        Debug.Log("Turn left");
    }

    /// <summary>
    /// Turn the tank right
    /// </summary>
    protected void TurnRight()
    {
        gameObject.transform.rotation = lookRight;
        moveForward = gameObject.transform.up * speed;
        Debug.Log("Turn right");
    }

    /// <summary>
    /// Move the tank forward
    /// </summary>
    protected void MoveForward()
    {
        rb.velocity = moveForward;
        SetAnimation(true);
        // Debug.Log("Move forward");
    }

    /// <summary>
    /// Stop the tank
    /// </summary>
    protected void Stop()
    {
        rb.velocity = new Vector3(0, 0, 0);
        SetAnimation(false);
    }

    /// <summary>
    /// Shoot the bullets
    /// </summary>
    /// <param name="playerOrigin">Is the bullet shoot by the player</param>
    protected void Shoot(bool playerOrigin)
    {
        GameObject aBullet = null;
        while(aBullet == null){
            aBullet = BulletPooler.singleton.getABullet(shootingPos.transform.position, transform.rotation, true, playerOrigin);
        }
        Debug.Log("Shoot");
    }

    /// <summary>
    /// Set the animation when the tank is moving
    /// </summary>
    /// <param name="isRunning"></param>
    private void SetAnimation(bool isRunning){
        tankAnimator.SetBool("isRunning", isRunning);
    }

}
