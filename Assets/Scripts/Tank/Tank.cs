using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{
    [Header("Sound")]
    public AudioSource movingSource;
    public AudioSource shootingSource;
    public AudioClip movingClip, shootingClip;

    [Space]
    public GameObject shootingPos;
    public Animator tankAnimator;
    private Vector3 moveForward;
    private static Quaternion lookUp, lookDown, lookLeft, lookRight;
    protected Rigidbody2D rb;
    protected bool playerOrigin, powerUp;
    public float speed = 1f;

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
        PlayMovingSFX();
    }

    /// <summary>
    /// Turn the tank down
    /// </summary>
    protected void TurnDown()
    {
        gameObject.transform.rotation = lookDown;
        PlayMovingSFX();
    }

    /// <summary>
    /// Turn the tank left
    /// </summary>
    protected void TurnLeft()
    {
        gameObject.transform.rotation = lookLeft;
        PlayMovingSFX();
    }

    /// <summary>
    /// Turn the tank right
    /// </summary>
    protected void TurnRight()
    {
        gameObject.transform.rotation = lookRight;
        PlayMovingSFX();
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
        AudioController.Instance.PlaySFX(shootingSource, shootingClip);
        BulletPooler.singleton.GetClone(shootingPos.transform.position, transform.rotation, powerUp, gameObject.GetInstanceID(), playerOrigin);
    }

    /// <summary>
    /// Play sound effect when moving
    /// </summary>
    protected void PlayMovingSFX()
    {
        if (!movingSource.isPlaying)
            AudioController.Instance.PlaySFX(movingSource, movingClip);
    }

    /// <summary>
    /// Stop the moving sound effect
    /// </summary>
    protected void StopMovingSFX()
    {
        if (movingSource.isPlaying) movingSource.Stop();
    }

    /// <summary>
    /// Switch the running animation of the tank
    /// </summary>
    /// <param name="isRunning">Switch on of off</param>
    private void SetAnimation(bool isRunning)
    {
        tankAnimator.SetBool("isRunning", isRunning);
    }

    protected virtual void OnDisable()
    {
        ParticalController.Instance.GetClone(gameObject.transform.position, Partical.Destroy);
        StopMovingSFX();
    }
}
