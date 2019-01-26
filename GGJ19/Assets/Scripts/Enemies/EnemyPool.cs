using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : Singleton<EnemyPool>
{
    //Variables
    [SerializeField]
    private GameObject puffPrefab;
    private GameObject pillowPrefab;
    private GameObject mattressPrefab;

    [SerializeField]
    private int initialPoolSize = 20;

    private Queue<GameObject> puffObjects;
    private Queue<GameObject> pillowObjects;
    private Queue<GameObject> mattressObjects;

    private void Start()
    {
        puffObjects = new Queue<GameObject>();
        pillowObjects = new Queue<GameObject>();
        mattressObjects = new Queue<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(puffPrefab);
            obj.SetActive(false);
            puffObjects.Enqueue(obj);

            obj = Instantiate(pillowPrefab);
            obj.SetActive(false);
            pillowObjects.Enqueue(obj);

            obj = Instantiate(mattressPrefab);
            obj.SetActive(false);
            mattressObjects.Enqueue(obj);
        }
    }

    public void registerNewEnemy(GameObject enemy, enemyType enemyType)
    {
        if (enemyType == enemyType.Pillow) pillowObjects.Enqueue(enemy);
        else if (enemyType == enemyType.Puff) pillowObjects.Enqueue(enemy);
        else if(enemyType == enemyType.Mattress) mattressObjects.Enqueue(enemy);
    }

    public void spawnEnemy(Vector3 position, enemyType enemyType)
    {
        if (enemyType == enemyType.Pillow)
        {
            GameObject obj;
            if (pillowObjects.Count > 0) obj = pillowObjects.Dequeue();
            else obj = Instantiate(pillowPrefab);

            obj.transform.position = position;
            obj.SetActive(true);
        }
        else if (enemyType == enemyType.Puff)
        {
            GameObject obj;
            if (puffObjects.Count > 0) obj = puffObjects.Dequeue();
            else obj = Instantiate(puffPrefab);

            obj.transform.position = position;
            obj.SetActive(true);
        }
        else if (enemyType == enemyType.Mattress)
        {
            GameObject obj;
            if (mattressObjects.Count > 0) obj = mattressObjects.Dequeue();
            else obj = Instantiate(mattressPrefab);

            obj.transform.position = position;
            obj.SetActive(true);
        }
    }
}
