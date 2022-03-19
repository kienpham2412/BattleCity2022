using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Tank
{
    public static Player Instance;
    private PlayerControl playerControl;
    private InputAction movement;
    private InputAction shoot;
    private Vector2 direction;
    private const float POWERUP_LENGTH = 15f;
    private float xDirection, yDirection;


    void Awake()
    {
        Instance = this;

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

    public Coordinate GetCoordinate(){
        return Coordinate.GetCurrentCoordinate(transform.position);
    }

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

    IEnumerator PowerUp()
    {
        powerUp = true;
        yield return new WaitForSeconds(POWERUP_LENGTH);
        powerUp = false;
    }

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
