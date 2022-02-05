using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public static float speed = 0.5f;
    private static float movementSpeed;
    private static Vector3 moveUp, moveDown, moveLeft, moveRight;
    // // Start is called before the first frame update
    void Start()
    {
        movementSpeed = speed * Time.deltaTime;
        moveUp = Vector3.up * movementSpeed;
        moveDown = Vector3.down * movementSpeed;
        moveLeft = Vector3.left * movementSpeed;
        moveRight = Vector3.right * movementSpeed;
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    protected void MoveUp(){
        gameObject.transform.Translate(moveUp, Space.World);
        transform.rotation = Quaternion.LookRotation(Vector2.up);
        Debug.Log("Move up");
    }
    protected void MoveDown(){
        gameObject.transform.Translate(moveDown, Space.World);
        transform.rotation = Quaternion.LookRotation(Vector2.down);
        Debug.Log("Move down");
    }
    protected void MoveLeft(){
        gameObject.transform.Translate(moveLeft, Space.World);
        transform.rotation = Quaternion.LookRotation(Vector2.left);
        Debug.Log("Move left");
    }
    protected void MoveRight(){
        gameObject.transform.Translate(moveRight, Space.World);
        transform.rotation = Quaternion.LookRotation(Vector2.right);
        Debug.Log("Move right");
    }
    protected void Shoot(){
        Debug.Log("Shoot");
    }

}
