using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour
{
    public string hittableTag;

    public List<GameObject> hittableObjects = new List<GameObject>();




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

}
