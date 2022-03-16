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
    protected GameObject thisGameObject;
    protected Rigidbody2D enemyRB;
    protected Marker marker;
    protected Vector3 markerPosition;
    protected Vector3 moveForward;
    protected Animator tankAnimator;
    protected float angle;
    protected float speed = 1f;

    public State(GameObject thisGameObject, Marker marker, Rigidbody2D enemyRB, Animator tankAnimator)
    {
        this.marker = marker;
        this.thisGameObject = thisGameObject;
        this.enemyRB = enemyRB;
        this.tankAnimator = tankAnimator;
        this.stateEvent = EVENT.ENTER;
    }

    public virtual void Enter()
    {
        Debug.Log($"Begin {name} state");
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
        moveForward = thisGameObject.transform.up * speed;
        enemyRB.velocity = moveForward;
    }

    protected void MoveToDestination()
    {
        float distance = Vector3.Distance(thisGameObject.transform.position, markerPosition);

        if (distance > 0.01f)
        {
            MoveForward();
            return;
        }

        thisGameObject.transform.position = markerPosition;
        marker = marker.parent;

        if (marker != null)
        {
            markerPosition = Coordinate.ToVector3(marker.coordinate);
            Vector3 dirToMarker = markerPosition - thisGameObject.transform.position;
            angle = Vector3.SignedAngle(thisGameObject.transform.up, dirToMarker, thisGameObject.transform.forward);
            thisGameObject.transform.Rotate(0, 0, angle);
        }
        else
        {
            stateEvent = EVENT.EXIT;
            Debug.Log("reach destination");
        }
    }

    protected void Stop()
    {
        enemyRB.velocity = new Vector2(0, 0);
        SetAnimation(false);
    }

    protected void SetAnimation(bool isRunning)
    {
        tankAnimator.SetBool("isRunning", isRunning);
    }
}

public class Idle : State
{
    public Idle(GameObject thisGameObject, Marker marker, Rigidbody2D enemyRB, Animator tankAnimator) : base(thisGameObject, marker, enemyRB, tankAnimator)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        stateEvent = EVENT.EXIT;
    }

    public override void Exit()
    {
        nextState = new Wander(thisGameObject, marker, enemyRB, tankAnimator);
    }
}

public class Patrol : State
{
    public Patrol(GameObject thisGameObject, Marker marker, Rigidbody2D enemyRB, Animator tankAnimator) : base(thisGameObject, marker, enemyRB, tankAnimator)
    {
        name = STATE.PATROL;
    }

    public override void Enter()
    {
        marker = PathFinder.singleton.FindPath(Map.playerSpawnRight, new Coordinate(thisGameObject.transform.position));
        markerPosition = Coordinate.ToVector3(marker.coordinate);
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
        nextState = new Idle(thisGameObject, marker, enemyRB, tankAnimator);
        base.Exit();
    }
}

public class Wander : State
{
    public Wander(GameObject thisGameObject, Marker marker, Rigidbody2D enemyRB, Animator tankAnimator) : base(thisGameObject, marker, enemyRB, tankAnimator)
    {
        name = STATE.WANDER;
    }

    public override void Enter()
    {
        Coordinate current = new Coordinate(thisGameObject.transform.position);
        Coordinate destination = PathFinder.singleton.GetNextCoordinate(current);

        if (destination == null)
        {
            stateEvent = EVENT.EXIT;
            return;
        }

        Marker destinationMarker = new Marker(destination, null);
        marker = new Marker(current, destinationMarker);
        markerPosition = Coordinate.ToVector3(marker.coordinate);
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
        nextState = new Idle(thisGameObject, marker, enemyRB, tankAnimator);
        base.Exit();
    }
}
