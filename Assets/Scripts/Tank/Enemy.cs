using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Tank
{
    private Marker marker;
    private Vector3 markerPosition;
    private float angle;
    private bool isTracking = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        playerOrigin = false;
        powerUp = false;

        base.Start();
        marker = PathFinder.singleton.FindPath(Map.enemySpawnMid, new Coordinate(gameObject.transform.position));
        markerPosition = marker.coordinate.ToVector3();
        // TurnDown();
        Invoke("PathToTower", 5f);
    }

    private void Update()
    {
        if (isTracking)
        {
            float distance = Vector3.Distance(gameObject.transform.position, markerPosition);
            if (distance <= 0.01f)
            {
                gameObject.transform.position = markerPosition;
                marker = marker.parent;
                if (marker != null)
                {
                    markerPosition = marker.coordinate.ToVector3();
                    Vector3 dirToMarker = markerPosition - gameObject.transform.position;
                    angle = Vector3.SignedAngle(gameObject.transform.up, dirToMarker, gameObject.transform.forward);
                    gameObject.transform.Rotate(0, 0, angle);
                }
                else
                {
                    isTracking = false;
                    Stop();
                    Debug.Log("reach destination");
                    return;
                }
            }
            else
            {
                MoveForward();
            }
        }
    }

    private void PathToTower()
    {
        isTracking = true;
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

    // private void OnEnable()
    // {
    //     InvokeRepeating("AutoShoot", 1, 1);
    // }

    protected override void OnDisable()
    {
        // CancelInvoke();
        base.OnDisable();
    }
}
