using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Variables
    private Vector3 direction;
    private float currentTimer;
    [SerializeField]
    private float movementSpeed;

    private void disableBullet()
    {
        BulletPool.Instance.registerNewBullet(this.gameObject);
        this.gameObject.SetActive(false);
    }

    //Set Info on Enable
    public void setInfo(Vector3 position, Vector3 direction, float timeToDie)
    {
        this.transform.position = position;
        currentTimer = timeToDie;
        this.direction = direction;
        this.transform.right = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        var player = collision.GetComponent<PlayerStateController>();
        if (player != null)
        {
            if (player.TakeHit(transform.position))
            {
                disableBullet();
            }
        }
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;
        if(currentTimer <= 0f)
        {
            disableBullet();
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + direction, movementSpeed * Time.deltaTime);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0f);
        }
        
    }
}
