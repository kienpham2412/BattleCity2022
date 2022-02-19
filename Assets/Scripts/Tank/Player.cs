using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Tank
{
    public static Player singleton;
    private PlayerControl playerControl;
    private InputAction movement;
    private InputAction shoot;
    private Vector2 direction;
    private const float POWERUP_LENGTH = 15f;
    private float xDirection, yDirection;


    void Awake()
    {
        singleton = GetComponent<Player>();

        playerControl = new PlayerControl();
        movement = playerControl.Gameplay.Movement;
        shoot = playerControl.Gameplay.Shoot;

        movement.performed += ctx => Move();
        movement.canceled += ctx => { xDirection = yDirection = 0; };
        shoot.performed += ctx => Shoot(playerOrigin);

        playerOrigin = true;
        powerUp = false;

        Debug.Log("tank player awake");
    }

    void Update()
    {
        if (xDirection != 0 || yDirection != 0)
        {
            MoveForward();
        }
        else
        {
            Stop();
        }
    }

    /// <summary>
    /// Enable and disable the input of the tank control
    /// </summary>
    /// <param name="isActive"></param>
    public void ActiveInput(bool isActive)
    {
        if (isActive)
        {
            playerControl.Enable();
        }
        else
        {
            playerControl.Disable();
        }
    }

    /// <summary>
    /// Move the tank by control direction
    /// </summary>
    void Move()
    {
        direction = movement.ReadValue<Vector2>();
        xDirection = direction.x;
        yDirection = direction.y;
        if (yDirection == 1) TurnUp();
        if (yDirection == -1) TurnDown();
        if (xDirection == -1) TurnLeft();
        if (xDirection == 1) TurnRight();
    }

    /// <summary>
    /// Make the player tank more powerful
    /// </summary>
    /// <returns></returns>
    IEnumerator PowerUp()
    {
        powerUp = true;
        yield return new WaitForSeconds(POWERUP_LENGTH);
        powerUp = false;
    }

    /// <summary>
    /// This function is triggered when player collide with an item
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Star"))
        {
            StartCoroutine(PowerUp());
        }
    }

    private void OnEnable()
    {
        ActiveInput(true);
    }
    
    protected override void OnDisable()
    {
        ActiveInput(false);
        base.OnDisable();
    }
}
