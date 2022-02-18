using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Tank
{
    private GameObject tracker;
    private TrackerBehav trackerBehav;
    private Vector3 dirToTracker;
    private float distance;
    private int dirNum;
    // private bool firstTime = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        playerOrigin = false;
        powerUp = false;

        base.Start();
        // TurnDown();
    }

    void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position, tracker.transform.position);
        // Debug.Log(distance);
        if (distance == 0)
        {
            trackerBehav.MoveNext();
        }
        else
        {
            // dirToTracker = gameObject.transform.position - tracker.transform.position;
            // Quaternion look = Quaternion.LookRotation(dirToTracker, Vector3.right);
            // gameObject.transform.rotation = look;
            // MoveForward();
        }
    }

    public void ActiveFreezing()
    {
        StartCoroutine(Freeze());
    }



    private void AutoShoot()
    {
        base.Shoot(playerOrigin);
    }

    IEnumerator Freeze()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(10f);
        rb.constraints = RigidbodyConstraints2D.None;
    }

    private void OnEnable()
    {
        // if (firstTime)
        // {
        //     firstTime = false;
        //     return;
        // }
        // InvokeRepeating("AutoShoot", 1, 1);
        tracker = Referee.singleton.GetTankTracker(gameObject.transform.position);
        trackerBehav = tracker.GetComponent<TrackerBehav>();
    }

    protected override void OnDisable()
    {
        // CancelInvoke();
        base.OnDisable();
        if (tracker != null)
        {
            tracker.SetActive(false);
            tracker = null;
            trackerBehav = null;
        }
    }
}
