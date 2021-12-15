using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int x;
    public int y;

    public void SetCoordinate(int x, int y){
        this.x = x;
        this.y = y;
    }
}
