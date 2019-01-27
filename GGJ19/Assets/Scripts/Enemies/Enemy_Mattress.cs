using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mattress : Enemy
{
    //Variables
    [SerializeField]
    private int maxHP_Mattress;
    [SerializeField]
    private float shotRatio;
    private float currentShotTimer;
    [SerializeField]
    private float detectionRange;
    private Collider2D collision;

    private void OnEnable()
    {
        base.currentHP = maxHP_Mattress;
        currentState = enemyState.Active;
        animatorRef.enabled = true;
        boxCollider2DRef.enabled = true;
        rigidBody2DRef.isKinematic = false;
        spriteRendererRef.sortingOrder = 0;
        animatorRef.Play("Idle");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            LudicController.Instance.ludicMeter--;
            //Player stun
        }
    }

    private void Update()
    {
        if(currentShotTimer > 0f) currentShotTimer -= Time.deltaTime;
        else if (currentShotTimer <= 0f)
        {
            collision = Physics2D.OverlapCircle(this.transform.position, detectionRange, playerLayerMask);
            if(collision != null)
            {
                if(collision.transform.position.x > this.transform.position.x) this.transform.GetChild(0).localScale = new Vector3(-1f, 1f, 1f);
                else this.transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);

                animatorRef.SetTrigger("attack");
                currentShotTimer = shotRatio;
                BulletPool.Instance.spawnBullet(this.transform.position, (collision.transform.position - this.transform.position).normalized, 3f);
            }
        }
    }
}
