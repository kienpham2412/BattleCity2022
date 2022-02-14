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
    private float xDirection, yDirection;
    

    void Awake() {
        singleton = GetComponent<Player>();

        playerControl = new PlayerControl();
        movement = playerControl.Gameplay.Movement;
        shoot = playerControl.Gameplay.Shoot;

        movement.performed += ctx => Move();
        movement.canceled += ctx => {xDirection = yDirection = 0;};
        shoot.performed += ctx => Shoot(playerOrigin);

        playerOrigin = true;
        powerUp = true;

        Debug.Log("tank player awake");

        ActiveInput(true);
    }

    void Update() {
        if(xDirection != 0 || yDirection != 0){
            MoveForward();
        } else {
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
    void Move(){
        direction = movement.ReadValue<Vector2>();
        xDirection = direction.x;
        yDirection = direction.y;
        if(yDirection == 1) TurnUp();
        if(yDirection == -1) TurnDown();
        if(xDirection == -1) TurnLeft();
        if(xDirection == 1) TurnRight();
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled.
    /// </summary>
    protected override void OnDisable() {
        ActiveInput(false);
        base.OnDisable();
    }
}
