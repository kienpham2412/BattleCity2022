using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData
{
    public EnemyData(GameObject enemyGO, Marker marker, Rigidbody2D enemyRB, Animator enemyAnimator, Coordinate previous)
    {
        this.enemyGO = enemyGO;
        this.marker = marker;
        this.enemyRB = enemyRB;
        this.enemyAnimator = enemyAnimator;
        this.previous = previous;
    }

    public GameObject enemyGO;
    public Marker marker;
    public Rigidbody2D enemyRB;
    public Animator enemyAnimator;
    public Coordinate previous;
}

public class EnemyAI : Tank, ISubscriber
{
    EnemyData enemyData;
    private Marker marker;
    private Vector3 spawnPosition;
    private State currentState;
    public Coordinate previous;
    private PathFinder pathFinder;
    private IEnumerator attackRoutine;
    private const float TIME_SHOOTING_LIMIT = 2;
    private float shootingSpeed = 1.5f;
    private float startShooting = 0;

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case MessageType.OnGrenadeAcquired:
                gameObject.SetActive(false);
                break;
            case MessageType.OnClockAcquired:
                StartCoroutine(Freeze());
                break;
            case MessageType.OnGameRestart:
                pathFinder = FindObjectOfType<PathFinder>();
                previous = new Coordinate(gameObject.transform.position);
                rb = GetComponent<Rigidbody2D>();
                enemyData = new EnemyData(gameObject, marker, rb, tankAnimator, previous);
                currentState = new Idle(enemyData, pathFinder);
                playerOrigin = false;
                powerUp = false;
                break;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(shootingPos.transform.position, transform.up, new Vector3(3, 0, 0).magnitude);

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
        if (firstTime)
        {
            firstTime = false;
            return;
        }
        ParticalController.Instance.GetClone(gameObject.transform.position, Partical.Destroy);
        MessageManager.Instance.SendMessage(new Message(MessageType.OnEnemyDestroyed, gameObject.transform.position));
    }

    private void OnEnable()
    {
        spawnPosition = gameObject.transform.position;
        MessageManager.Instance.AddSubscriber(MessageType.OnGrenadeAcquired, this);
        MessageManager.Instance.AddSubscriber(MessageType.OnClockAcquired, this);
        MessageManager.Instance.AddSubscriber(MessageType.OnGameRestart, this);
    }
}