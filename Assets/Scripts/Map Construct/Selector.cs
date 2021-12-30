using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Selector : MonoBehaviour
{
    public static Selector singleton;
    public Vector2 direction;
    private Transform child;
    private SpriteRenderer mySprite;
    public List<Sprite> sprites;
    private Vector3 position;
    private MapBuilder mapBuilder;
    private PlayerControl control;
    private InputAction movement;
    private InputAction selectObstacle;
    private InputAction placeObstacle;
    private int obstacleIndex;

    private void Awake()
    {
        obstacleIndex = 0;

        singleton = GetComponent<Selector>();
        mapBuilder = GameObject.Find("MapConstructor").GetComponent<MapBuilder>();
        child = gameObject.transform.GetChild(0);
        mySprite = child.gameObject.GetComponent<SpriteRenderer>();
        mySprite.sprite = sprites[obstacleIndex];

        control = new PlayerControl();
        movement = control.MapBuilding.Movement;
        selectObstacle = control.MapBuilding.SelectObstacle;
        placeObstacle = control.MapBuilding.PlaceObstacle;

        movement.performed += ctx => Move();
        selectObstacle.performed += ctx => Select();
        placeObstacle.performed += ctx => PlaceObstacle();

        ActiveInput(true);

        // Debug.Log("selector size: " + gameObject.GetComponent<SpriteRenderer>().bounds.size.x);
    }

    public void ActiveInput(bool isActive)
    {
        if (isActive)
        {
            control.Enable();
        }
        else
        {
            control.Disable();
        }
    }

    private void PlaceObstacle()
    {
        mapBuilder.Replace(new Coordinate((int)transform.position.x, (int)transform.position.y), obstacleIndex);
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
        int dir = (int)value;
        obstacleIndex += dir;

        if (obstacleIndex < 0) obstacleIndex = mapBuilder.obstacles.Count - 1;
        else if (obstacleIndex >= mapBuilder.obstacles.Count) obstacleIndex = 0;
        Debug.Log(obstacleIndex);

        mySprite.sprite = sprites[obstacleIndex];
    }

    private void OnDisable()
    {
        ActiveInput(false);
    }
}
