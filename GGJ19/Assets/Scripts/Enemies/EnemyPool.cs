using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : Singleton<EnemyPool>
{
    //Variables
    [SerializeField]
    private GameObject puffPrefab;
    [SerializeField]
    private GameObject pillowPrefab;
    [SerializeField]
    private GameObject mattressPrefab;

    [SerializeField]
    private int initialPoolSize = 20;

    private Queue<GameObject> puffObjects;
    private Queue<GameObject> pillowObjects;
    private Queue<GameObject> mattressObjects;
    public LinkedList<GameObject> activeEnemies;
    public LinkedList<GameObject> defeatedEnemies;
    
    private void Start()
    {
        base.Awake();
        puffObjects = new Queue<GameObject>();
        pillowObjects = new Queue<GameObject>();
        mattressObjects = new Queue<GameObject>();
        activeEnemies = new LinkedList<GameObject>();
        defeatedEnemies = new LinkedList<GameObject>();

        Debug.Log("PreFOR");

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

            Debug.Log("FOR");
        }

        Debug.Log("Complete");
    }

    public void defeatEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        defeatedEnemies.AddLast(enemy);
    }

    public void cleanUpEnemies()
    {
        foreach(GameObject obj in defeatedEnemies)
        {
            if (obj.GetComponent<Enemy>() is Enemy_Pillow) pillowObjects.Enqueue(obj);
            else if (obj.GetComponent<Enemy>() is Enemy_Puff) puffObjects.Enqueue(obj);
            else if (obj.GetComponent<Enemy>() is Enemy_Mattress) mattressObjects.Enqueue(obj);
        }
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
            activeEnemies.AddLast(obj);
        }
        else if (enemyType == enemyType.Puff)
        {
            GameObject obj;
            if (puffObjects.Count > 0) obj = puffObjects.Dequeue();
            else obj = Instantiate(puffPrefab);

            obj.transform.position = position;
            obj.SetActive(true);
            activeEnemies.AddLast(obj);
        }
        else if (enemyType == enemyType.Mattress)
        {
            GameObject obj;
            if (mattressObjects.Count > 0) obj = mattressObjects.Dequeue();
            else obj = Instantiate(mattressPrefab);

            obj.transform.position = position;
            obj.SetActive(true);
            activeEnemies.AddLast(obj);
        }
    }
}
