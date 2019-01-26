﻿using System.Collections;
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
    private LayerMask playerLayerMask;
    private int playerLayer;

    //Methods
    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        playerLayerMask = LayerMask.GetMask("Player");
    }

    private void OnEnable()
    {
        base.currentHP = maxHP_Mattress;
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
                currentShotTimer = shotRatio;
                BulletPool.Instance.spawnBullet(this.transform.position, (collision.transform.position - this.transform.position).normalized, 3f);
            }
        }
    }
}