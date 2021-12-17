using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Selector : MonoBehaviour
{
    public static Selector singleton;
    public Vector2 direction;
    private Vector3 position;
    private PlayerControl control;
    private InputAction movement;
    private InputAction selectObstacle;

    private void Awake()
    {
        singleton = GetComponent<Selector>();

        control = new PlayerControl();
        movement = control.MapBuilding.Movement;
        selectObstacle = control.MapBuilding.SelectObstacle;

        movement.performed += ctx => Move();
        selectObstacle.performed += ctx => Select();

        ActiveInput(true);

        // Debug.Log("selector size: " + gameObject.GetComponent<SpriteRenderer>().bounds.size.x);
    }

    public void ActiveInput(bool isActive)
    {
        if (isActive)
        {
            control.Enable();
            // movement.Enable();
            // selectObstacle.Enable();
        }
        else
        {
            control.Disable();
            // movement.Disable();
            // selectObstacle.Disable();
        }
    }

    /// <summary>
    /// Move selector to the direction of input
    /// </summary>
    private void Move()
    {
        direction = movement.ReadValue<Vector2>();
        gameObject.transform.Translate(Verify(direction));

        position = transform.position;
        if (position.x < 0) gameObject.transform.position = new Vector3(12, position.y, 0);
        if (position.x > 12) gameObject.transform.position = new Vector3(0, position.y, 0);
        if (position.y < 0) gameObject.transform.position = new Vector3(position.x, 12, 0);
        if (position.y > 12) gameObject.transform.position = new Vector3(position.x, 0, 0);
        // Debug.Log("direction: " + direction.x + "-" + direction.y);
    }

    /// <summary>
    /// Round the coordinate of an vector
    /// </summary>
    /// <param name="dir">Vector's value to be rounded</param>
    /// <returns>New Vector with rounded coordinate</returns>
    private Vector2 Verify(Vector2 dir)
    {
        dir.x = Mathf.Round(dir.x);
        dir.y = Mathf.Round(dir.y);
        return new Vector2(dir.x, dir.y);
    }

    /// <summary>
    /// Select obstacles
    /// </summary>
    private void Select()
    {
        float value = selectObstacle.ReadValue<float>();
        Debug.Log(value);
    }

    private void OnDisable()
    {
        ActiveInput(false);
    }
}
