using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData
{
    public EnemyData(GameObject enemyGO, Marker marker, Rigidbody2D enemyRB, Animator enemyAnimator, Coordinate previous, float speed)
    {
        this.enemyGO = enemyGO;
        this.marker = marker;
        this.enemyRB = enemyRB;
        this.enemyAnimator = enemyAnimator;
        this.previous = previous;
        this.speed = speed;
    }

    public GameObject enemyGO;
    public Marker marker;
    public Rigidbody2D enemyRB;
    public Animator enemyAnimator;
    public Coordinate previous;
    public float speed;
}

public class EnemyAI : Tank, ISubscriber, IBlock
{
    EnemyData enemyData;
    private Marker marker;
    private Vector3 spawnPosition;
    private State currentState;
    private Coordinate previous;
    private PathFinder pathFinder;
    private IEnumerator attackRoutine;
    private const float TIME_SHOOTING_LIMIT = 2;
    private const float SHOOTING_RANGE = 4;
    private float shootingSpeed = 1.5f;
    private float startShooting = 0;
    public int health;

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case MessageType.OnGrenadeAcquired:
                Destroy(gameObject);
                break;
            case MessageType.OnClockAcquired:
                if (gameObject.activeInHierarchy)
                    StartCoroutine(Freeze());
                break;
            case MessageType.OnGameRestart:
                Init();
                break;
        }
    }

    public void TakeDamage(DamageAttribute damageAttribute)
    {
        health -= damageAttribute.damage;
        if (health <= 0) Destroy(gameObject);
    }

    private void Init()
    {
        pathFinder = FindObjectOfType<PathFinder>();
        previous = new Coordinate(gameObject.transform.position);
        rb = GetComponent<Rigidbody2D>();
        enemyData = new EnemyData(gameObject, marker, rb, tankAnimator, previous, speed);
        currentState = new Idle(enemyData, pathFinder);
        playerOrigin = false;
        powerUp = false;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(shootingPos.transform.position, transform.up, SHOOTING_RANGE);

        if (hit.collider != null)
        {
            AutoShoot(shootingSpeed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
            currentState = currentState.Process();
    }

    private void AutoShoot(float speed)
    {
        startShooting += speed;
        if (startShooting >= TIME_SHOOTING_LIMIT)
        {
            Shoot(playerOrigin);
            startShooting = 1;
        }
    }

    IEnumerator Freeze()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(10f);
        rb.constraints = RigidbodyConstraints2D.None;
    }

    protected override void OnDisable()
    {
        MessageManager.Instance.RemoveSubscriber(MessageType.OnGrenadeAcquired, this);
        MessageManager.Instance.RemoveSubscriber(MessageType.OnClockAcquired, this);
        MessageManager.Instance.RemoveSubscriber(MessageType.OnGameRestart, this);

        if (health == 0)
        {
            ParticalController.Instance.GetClone(gameObject.transform.position, Partical.Destroy);
            MessageManager.Instance.SendMessage(new Message(MessageType.OnEnemyDestroyed, spawnPosition));
            TankShowCase.Instance.Destroy();
        }
    }

    private void OnEnable()
    {
        spawnPosition = gameObject.transform.position;
        Init();
        MessageManager.Instance.AddSubscriber(MessageType.OnGrenadeAcquired, this);
        MessageManager.Instance.AddSubscriber(MessageType.OnClockAcquired, this);
        MessageManager.Instance.AddSubscriber(MessageType.OnGameRestart, this);
    }
}