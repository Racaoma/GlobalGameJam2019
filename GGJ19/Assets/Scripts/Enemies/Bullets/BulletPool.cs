using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : Singleton<BulletPool>
{
    //Variables
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private int initialPoolSize = 20;
    private Queue<GameObject> bulletObjects;

    //Methods
    private void Start()
    {
        bulletObjects = new Queue<GameObject>();
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            bulletObjects.Enqueue(obj);
        }
    }

    public void registerNewBullet(GameObject bullet)
    {
        bulletObjects.Enqueue(bullet);
    }

    public void spawnBullet(Vector3 center, Vector3 direction, float timeToDie)
    {
        //Get Bullet
        GameObject obj;
        if (bulletObjects.Count > 0) obj = bulletObjects.Dequeue();
        else obj = Instantiate(bulletPrefab);

        //Set Bullet Info
        obj.SetActive(true);
        obj.GetComponent<Bullet>().setInfo(center, direction, timeToDie);
    }
}
