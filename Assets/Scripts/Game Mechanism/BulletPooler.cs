using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    public static BulletPooler singleton;
    public GameObject bulletSample;
    public GameObject bulletPool;
    private List<GameObject> bullets;

    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
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
            GameObject bullet = Instantiate(bulletSample, transform.position, Quaternion.identity, bulletPool.transform);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }

    public GameObject getABullet(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeSelf)
            {
                bullet.transform.position = position;
                bullet.transform.rotation = rotation;
                bullet.SetActive(true);
                return bullet;
            }
        }
        return null;
    }
}
