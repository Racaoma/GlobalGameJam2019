﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour
{
    public string hittableTag;

    public List<GameObject> hittableObjects = new List<GameObject>();



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Attack");
            HitObjects();
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TriggerEnter2D");

        if (collision.tag == hittableTag)
        {
            hittableObjects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("TriggerExit2D");

        if (collision.tag == hittableTag)
        {
            hittableObjects.Remove(collision.gameObject);
        }
    }

    public void HitObjects()
    {
        Debug.Log("HitObjects");

        List<CharacterHits> enemies = new List<CharacterHits>();

        for (int i=0; i < hittableObjects.Count; i++)
        {
            var enemy = hittableObjects[i].GetComponent<CharacterHits>();

            if (enemy != null)
            {
                enemies.Add(enemy);
            }
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].TakeHit();
        }
    }

}
