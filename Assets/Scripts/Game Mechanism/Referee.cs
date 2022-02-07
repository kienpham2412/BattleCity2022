using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Referee : MonoBehaviour
{
    private MapBuilder mapBuilder;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.singleton.LoadMap("7222");
        mapBuilder = GameManager.singleton.mapBuilder;
        mapBuilder.Replace(Map.enemySpawnPos1, 0);
        mapBuilder.Replace(Map.enemySpawnPos2, 0);
        mapBuilder.Replace(Map.enemySpawnPos3, 0);
        mapBuilder.Replace(Map.playerSpawnPos1, 0);
        mapBuilder.Replace(Map.playerSpawnPos2, 0);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
