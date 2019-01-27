using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fortress : Singleton<Fortress>
{

    public Transform[] fortresses;

    public void ActivateFortress(int index)
    {

        for (int i = 0; i < fortresses.Length; i++)
        {
            fortresses[i].gameObject.SetActive(i == index);
        }
    }

}
