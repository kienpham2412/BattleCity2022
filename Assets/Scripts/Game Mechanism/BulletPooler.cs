using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    public static BulletPooler singleton;
    public GameObject regularBullet, powerBullet;
    public GameObject bulletPool;
    private List<GameObject> regularList, powerList;

    // Start is called before the first frame update
    void Start()
    {
        regularList = new List<GameObject>();
        powerList = new List<GameObject>();
        singleton = GetComponent<BulletPooler>();

        CreatePool();
    }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    private void CreatePool()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateBullet(regularBullet, false);
            CreateBullet(powerBullet, true);
        }
    }

    private void CreateBullet(GameObject sample, bool powerUp)
    {
        GameObject bullet = Instantiate(sample, transform.position, Quaternion.identity, bulletPool.transform);
        bullet.SetActive(false);
        if (powerUp)
        {
            powerList.Add(bullet);
        }
        else
        {
            regularList.Add(bullet);
        }
    }

    public GameObject getABullet(Vector3 position, Quaternion rotation, bool powerUp, bool playerOrigin = true)
    {
        if (powerUp)
        {
            foreach (GameObject bullet in powerList)
            {
                if (!bullet.activeSelf)
                {
                    bullet.transform.position = position;
                    bullet.transform.rotation = rotation;
                    bullet.GetComponent<PowerUpBullet>().setPlayerOrigin(playerOrigin);
                    bullet.SetActive(true);
                    return bullet;
                }
            }
        }
        else
        {
            foreach (GameObject bullet in regularList)
            {
                if (!bullet.activeSelf)
                {
                    bullet.transform.position = position;
                    bullet.transform.rotation = rotation;
                    bullet.GetComponent<PowerUpBullet>().setPlayerOrigin(playerOrigin);
                    bullet.SetActive(true);
                    return bullet;
                }
            }
        }

        return null;
    }
}
