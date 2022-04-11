using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Tank, ISubscriber, IBlock
{
    [Header("Item SFX")]
    public AudioSource source;
    public AudioClip clip;

    [Header("Player attribute")]
    [SerializeField] private GameObject shieldFX;
    public float immotalTime;
    public int health;

    public static Player Instance;
    private PlayerControl playerControl;
    private InputAction movement;
    private InputAction shoot;
    private Vector2 direction;
    private const float POWERUP_LENGTH = 15f;
    private float xDirection, yDirection;
    private bool immotal;

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
                Destroy(gameObject);
                break;
            case MessageType.OnPlayerDestroyed:
                ParticalController.Instance.GetClone(gameObject.transform.position, Partical.Destroy);
                break;
        }

    }

    private void SetImmmotal(bool isImmotal)
    {
        immotal = isImmotal;
        shieldFX.SetActive(isImmotal);
    }

    IEnumerator ImmotalActive()
    {
        SetImmmotal(true);
        yield return new WaitForSeconds(immotalTime);
        SetImmmotal(false);
    }

    public void Immotal()
    {
        StartCoroutine(ImmotalActive());
    }

    public void TakeDamage(DamageAttribute damageAttribute)
    {
        if (immotal) return;

        health -= damageAttribute.damage;

        if (health <= 0)
        {
            Referee.Instance.playerLife--;
            MessageManager.Instance.SendMessage(new Message(MessageType.OnPlayerDestroyed));

            if (Referee.Instance.playerLife < 0)
                MessageManager.Instance.SendMessage(new Message(MessageType.OnGameOver));

            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (xDirection != 0 || yDirection != 0)
            MoveForward();
        else
            Stop();
    }

    public void ActiveInput(bool isActive)
    {
        if (isActive)
            playerControl.Enable();
        else
            playerControl.Disable();
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
        StartCoroutine(ImmotalActive());

        MessageManager.Instance.AddSubscriber(MessageType.OnGameRestart, this);
        MessageManager.Instance.AddSubscriber(MessageType.OnPlayerDestroyed, this);
        ActiveInput(true);
    }

    protected override void OnDisable()
    {
        MessageManager.Instance.RemoveSubscriber(MessageType.OnGameRestart, this);
        MessageManager.Instance.RemoveSubscriber(MessageType.OnPlayerDestroyed, this);

        if (health == 0) base.OnDisable();
        ActiveInput(false);
        StopMovingSFX();
    }
}
