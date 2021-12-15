using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
