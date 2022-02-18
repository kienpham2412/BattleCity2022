using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerBehav : MonoBehaviour
{
    private Coordinate currentCoordinate;
    public int dirNum;
    // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }
    public void MoveNext()
    {
        Coordinate dir;
        Coordinate newCoordinate;
        do
        {
            dir = PickDirection();
            newCoordinate = currentCoordinate + dir;
        }
        while (!newCoordinate.IsInsideMap(13));
        currentCoordinate = newCoordinate;
        gameObject.transform.position = currentCoordinate.ToVector3();
    }

    private Coordinate PickDirection()
    {
        dirNum = Random.Range(1, 4);
        Coordinate dir = new Coordinate();
        switch (dirNum)
        {
            case 1:
                dir = Coordinate.up;
                break;
            case 2:
                dir = Coordinate.down;
                break;
            case 3:
                dir = Coordinate.left;
                break;
            case 4:
                dir = Coordinate.right;
                break;
            default:
                break;
        }
        return dir;
    }

    private void OnEnable()
    {
        int x = (int)gameObject.transform.position.x;
        int y = (int)gameObject.transform.position.y;
        currentCoordinate = new Coordinate(x, y);
    }
}
