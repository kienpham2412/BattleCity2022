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
        shoot.performed += ctx => Shoot();

        Debug.Log("tank player awake");

        ActiveInput(true);
    }

    // private void Start() {
        
    // }

    void Update() {
        if(yDirection == 1) MoveUp();
        if(yDirection == -1) MoveDown();
        if(xDirection == -1) MoveLeft();
        if(xDirection == 1) MoveRight();
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

    void Move(){
        direction = movement.ReadValue<Vector2>();
        xDirection = direction.x;
        yDirection = direction.y;
    }

    // private void OnEnable() {
    //     playerControl.Gameplay.Enable();
    // }

    private void OnDisable() {
        ActiveInput(false);
    }
}
