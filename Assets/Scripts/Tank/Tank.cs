using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{
    public GameObject shootingPos;
    public Animator tankAnimator;
    private Vector3 moveForward;
    private static Quaternion lookUp, lookDown, lookLeft, lookRight;
    protected Rigidbody2D rb;
    protected bool playerOrigin, powerUp;
    public float speed = 1f;
    protected bool firstTime = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        lookUp = Quaternion.Euler(0, 0, 0);
        lookDown = Quaternion.Euler(0, 0, 180);
        lookLeft = Quaternion.Euler(0, 0, 90);
        lookRight = Quaternion.Euler(0, 0, -90);
    }

    /// <summary>
    /// Turn the tank up
    /// </summary>
    protected void TurnUp()
    {
        gameObject.transform.rotation = lookUp;
    }

    /// <summary>
    /// Turn the tank down
    /// </summary>
    protected void TurnDown()
    {
        gameObject.transform.rotation = lookDown;
    }

    /// <summary>
    /// Turn the tank left
    /// </summary>
    protected void TurnLeft()
    {
        gameObject.transform.rotation = lookLeft;
    }

    /// <summary>
    /// Turn the tank right
    /// </summary>
    protected void TurnRight()
    {
        gameObject.transform.rotation = lookRight;
    }

    /// <summary>
    /// Move the tank forward
    /// </summary>
    protected void MoveForward()
    {
        moveForward = gameObject.transform.up * speed;
        rb.velocity = moveForward;
        SetAnimation(true);
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
        BulletPooler.singleton.GetClone(shootingPos.transform.position, transform.rotation, powerUp, gameObject.GetInstanceID(), playerOrigin);
    }

    /// <summary>
    /// Set the animation when the tank is moving
    /// </summary>
    /// <param name="isRunning"></param>
    private void SetAnimation(bool isRunning)
    {
        tankAnimator.SetBool("isRunning", isRunning);
    }

    protected virtual void OnDisable()
    {
        if (firstTime)
        {
            firstTime = false;
            return;
        }
        ParticalController.Instance.GetClone(gameObject.transform.position, Partical.Destroy);
    }
}
