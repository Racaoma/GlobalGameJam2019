using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Variables
    private Vector3 direction;
    private float currentTimer;

    //Methods
    private void disableBullet()
    {
        BulletPool.Instance.registerNewBullet(this.gameObject);
        this.gameObject.SetActive(false);
    }

    //Set Info on Enable
    public void setInfo(Vector3 position, Vector3 direction, float timeToDie)
    {
        currentTimer = timeToDie;
        this.direction = direction;
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;
        if(currentTimer <= 0f)
        {
            disableBullet();
        }
    }
}
