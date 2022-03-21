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

public class EnemyAI : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
            currentState = currentState.Process();
    }

    protected virtual void OnDisable()
    {
        if (firstTime)
        {
            firstTime = false;
            return;
        }
        ParticalController.Instance.GetClone(gameObject.transform.position, Partical.Destroy);
        if (TankSpawner.Instance.CheckRemainingEnemy())
        {
            TankSpawner.Instance.GetClone(spawnPosition, Quaternion.identity);
        }
        TankSpawner.Instance.CountDestroyed();
    }

    private void OnEnable()
    {
        spawnPosition = gameObject.transform.position;
    }
}
