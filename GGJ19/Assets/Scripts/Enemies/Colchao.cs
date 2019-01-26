using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colchao : Enemy
{
    //Variables
    private int maxHP_Colchao = 3;
    private float shotRatio = 2f;
    private float currentShotTimer;
    private float detectionRange;
    private float detectionAngleThreshold;
    private Collider2D collision;
    private LayerMask playerLayerMask = LayerMask.GetMask("Player");

    //Methods
    private void OnEnable()
    {
        base.currentHP = maxHP_Colchao;
    }

    private void Update()
    {
        if(currentShotTimer > 0f) currentShotTimer -= Time.deltaTime;
        else if (currentShotTimer <= 0f)
        {
            collision = Physics2D.OverlapCircle(this.transform.position, detectionRange, playerLayerMask);
            if(collision != null)
            {
                if (Vector3.Angle(collision.transform.position, this.transform.position) <= detectionAngleThreshold)
                {
                    currentShotTimer = shotRatio;
                    BulletPool.Instance.spawnBullet(this.transform.position, collision.transform.position, 3f);
                }
            }
        }
    }
}
