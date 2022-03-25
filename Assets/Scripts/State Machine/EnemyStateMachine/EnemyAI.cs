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

public class EnemyAI : MonoBehaviour, ISubscriber
{
    EnemyData enemyData;
    public Animator tankAnimator;
    private Marker marker;
    private Rigidbody2D enemyRB;
    private Vector3 spawnPosition;
    private State currentState;
    public Coordinate previous;
    private PathFinder pathFinder;
    private bool firstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        pathFinder = FindObjectOfType<PathFinder>();
        enemyRB = GetComponent<Rigidbody2D>();
        previous = new Coordinate(gameObject.transform.position);
        enemyData = new EnemyData(gameObject, marker, enemyRB, tankAnimator, previous);
        currentState = new Idle(enemyData, pathFinder);

        MessageManager.Instance.AddSubscriber(MessageType.OnGrenadeAcquired, this);
        MessageManager.Instance.AddSubscriber(MessageType.OnClockAcquired, this);
    }

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
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
            currentState = currentState.Process();
    }

    IEnumerator Freeze()
    {
        enemyRB.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(10f);
        enemyRB.constraints = RigidbodyConstraints2D.None;
    }

    protected virtual void OnDisable()
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
    }
}
