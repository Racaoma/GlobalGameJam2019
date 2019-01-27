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
    private int playerLayer;
    private int groundLayer;
    
    //Methods
    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("Ground");
    }

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            LudicController.Instance.ludicMeter--;
            //Player stun
            disableBullet();
        }
        else if(collision.gameObject.layer == groundLayer)
        {
            //disableBullet();
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
        }
        
    }
}
