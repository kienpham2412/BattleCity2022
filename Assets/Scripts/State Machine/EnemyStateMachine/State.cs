using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public enum STATE
    {
        IDLE, PATROL, WANDER
    }
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }
    public STATE name;
    protected EVENT stateEvent;
    protected State nextState;
    protected PathFinder pathFinder;
    protected Vector3 markerPosition;
    protected Vector3 moveForward;
    protected EnemyData enemyData;
    protected float angle;
    protected float speed = 1f;

    public State(EnemyData enemyData, PathFinder pathFinder)
    {
        this.enemyData = enemyData;
        this.pathFinder = pathFinder;
        this.stateEvent = EVENT.ENTER;
    }

    public virtual void Enter()
    {
        stateEvent = EVENT.UPDATE;
    }

    public virtual void Update()
    {
        stateEvent = EVENT.UPDATE;
    }

    public virtual void Exit()
    {
        stateEvent = EVENT.EXIT;
    }

    public State Process()
    {
        if (stateEvent == EVENT.ENTER)
        {
            Enter();
        }
        if (stateEvent == EVENT.UPDATE)
        {
            Update();
        }
        if (stateEvent == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    public void MoveForward()
    {
        moveForward = enemyData.enemyGO.transform.up * speed;
        enemyData.enemyRB.velocity = moveForward;
    }

    protected void MoveToDestination()
    {
        float distance = Vector3.Distance(enemyData.enemyGO.transform.position, markerPosition);

        if (distance > 0.01f)
        {
            MoveForward();
            return;
        }

        enemyData.enemyGO.transform.position = markerPosition;
        enemyData.marker = enemyData.marker.parent;

        if (enemyData.marker != null)
        {
            markerPosition = Coordinate.ToVector3(enemyData.marker.coordinate);
            Vector3 dirToMarker = markerPosition - enemyData.enemyGO.transform.position;
            angle = Vector3.SignedAngle(enemyData.enemyGO.transform.up, dirToMarker, enemyData.enemyGO.transform.forward);
            enemyData.enemyGO.transform.Rotate(0, 0, angle);
        }
        else
        {
            stateEvent = EVENT.EXIT;
        }
    }

    protected void Stop()
    {
        enemyData.enemyRB.velocity = new Vector2(0, 0);
        SetAnimation(false);
    }

    protected void SetAnimation(bool isRunning)
    {
        enemyData.enemyAnimator.SetBool("isRunning", isRunning);
    }
}

public class Idle : State
{
    float distanceToTower;
    float distanceToPlayer;

    public Idle(EnemyData enemyData, PathFinder pathFinder) : base(enemyData, pathFinder)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        distanceToPlayer = Vector3.Distance(enemyData.enemyGO.transform.position, Player.Instance.transform.position);
        distanceToTower = Vector3.Distance(enemyData.enemyGO.transform.position, Coordinate.ToVector3(Map.tower));
        base.Enter();
    }

    public override void Update()
    {
        stateEvent = EVENT.EXIT;
    }

    public override void Exit()
    {
        if (distanceToTower <= PathFinder.TOWER_RANGE)
        {
            nextState = new Pursue(this.enemyData, Map.tower, pathFinder, PathFinder.TOWER_RANGE);
            Debug.Log("pursuing tower");
        }
        else if (distanceToPlayer <= PathFinder.PLAYER_RANGE)
        {
            nextState = new Pursue(this.enemyData, Player.Instance.GetCoordinate(), pathFinder, PathFinder.PLAYER_RANGE);
            Debug.Log("pursuing player");
        }
        else
        {
            nextState = new Patrol(enemyData, pathFinder);
        }
    }
}

public class Pursue : State
{
    Coordinate target;
    float limitRange;

    public Pursue(EnemyData enemyData, Coordinate target, PathFinder pathFinder, float limitRange) : base(enemyData, pathFinder)
    {
        name = STATE.PATROL;
        this.target = target;
        this.limitRange = limitRange;
    }

    public override void Enter()
    {
        enemyData.marker = pathFinder.FindPath(new Coordinate(enemyData.enemyGO.transform.position), target, limitRange);
        if (enemyData.marker == null)
        {
            stateEvent = EVENT.EXIT;
            nextState = new Patrol(enemyData, pathFinder);
            return;
        }

        markerPosition = Coordinate.ToVector3(enemyData.marker.coordinate);
        SetAnimation(true);
        base.Enter();
    }

    public override void Update()
    {
        MoveToDestination();
    }

    public override void Exit()
    {
        Stop();
        nextState = new Idle(enemyData, pathFinder);
        base.Exit();
    }
}

public class Patrol : State
{

    public Patrol(EnemyData enemyData, PathFinder pathFinder) : base(enemyData, pathFinder)
    {
        name = STATE.WANDER;
    }

    public override void Enter()
    {
        Coordinate current = new Coordinate(enemyData.enemyGO.transform.position);
        Coordinate destination = pathFinder.GetNextCoordinate(current, ref enemyData.previous);

        if (destination == null)
        {
            stateEvent = EVENT.EXIT;
            return;
        }

        Marker destinationMarker = new Marker(destination, null);
        enemyData.marker = new Marker(current, destinationMarker);
        markerPosition = Coordinate.ToVector3(enemyData.marker.coordinate);
        SetAnimation(true);
        base.Enter();
    }

    public override void Update()
    {
        MoveToDestination();
    }

    public override void Exit()
    {
        Stop();
        nextState = new Idle(enemyData, pathFinder);
        base.Exit();
    }
}
