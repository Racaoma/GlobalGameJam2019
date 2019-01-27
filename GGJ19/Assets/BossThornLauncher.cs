using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThornLauncher : MonoBehaviour
{

    public Thorn thornPrefab;
    public Transform headCenter;

    public List<Transform> thornAnchors;
    private List<Thorn> instantiatedThorns = new List<Thorn>();

    private void Start()
    {
        Thorn th;

        for (int i = 0; i < thornAnchors.Count; i++)
        {
            th = Instantiate(thornPrefab);
            th.gameObject.SetActive(false);
            instantiatedThorns.Add(th);
        }
    }

    public void LaunchThorns()
    {
        PositionThorns();
    }

    private void PositionThorns()
    {
        for (int i = 0; i < thornAnchors.Count; i++)
        {
            instantiatedThorns[i].gameObject.SetActive(true);
            instantiatedThorns[i].Reset();
            instantiatedThorns[i].transform.position = thornAnchors[i].position;
            instantiatedThorns[i].transform.rotation = Quaternion.LookRotation(thornAnchors[i].position - headCenter.position,Vector3.forward);
        }
    }

}
