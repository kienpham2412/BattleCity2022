using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerBlock))]
public class Player : Tank, ISubscriber
{
    [Header("Item SFX")]
    public AudioSource source;
    public AudioClip clip;

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
        movement.canceled += ctx => StopMoving();
        shoot.performed += ctx => Shoot(playerOrigin);

        playerOrigin = true;
        powerUp = false;

        Debug.Log("tank player awake");
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case MessageType.OnGameRestart:
                gameObject.SetActive(false);
                GetComponent<PlayerBlock>().ResetHealth();
                break;
            case MessageType.OnPlayerDestroyed:
                ParticalController.Instance.GetClone(gameObject.transform.position, Partical.Destroy);
                break;
        }

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

    public Coordinate GetCoordinate()
    {
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

    void StopMoving()
    {
        xDirection = yDirection = 0;
        StopMovingSFX();
    }

    public void TriggerPowerUp()
    {
        StartCoroutine(PowerUp());
    }

    IEnumerator PowerUp()
    {
        powerUp = true;
        yield return new WaitForSeconds(POWERUP_LENGTH);
        powerUp = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
            AudioController.Instance.PlaySFX(source, clip);
    }

    private void OnEnable()
    {
        MessageManager.Instance.AddSubscriber(MessageType.OnGameRestart, this);
        MessageManager.Instance.AddSubscriber(MessageType.OnPlayerDestroyed, this);
        ActiveInput(true);
    }

    protected override void OnDisable()
    {
        MessageManager.Instance.RemoveSubscriber(MessageType.OnGameRestart, this);
        MessageManager.Instance.RemoveSubscriber(MessageType.OnPlayerDestroyed, this);
        base.OnDisable();
        ActiveInput(false);
        StopMovingSFX();
    }
}
