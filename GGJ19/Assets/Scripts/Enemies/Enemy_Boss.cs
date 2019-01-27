using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class Enemy_Boss : Enemy
{
    //Variables
    private int maxHP_Boss = 20;
    private Collider2D collision;
    private LayerMask playerLayerMask;

    //Methods
    private void Start()
    {
        playerLayerMask = LayerMask.GetMask("Player");
    }

    private void OnEnable()
    {
        base.currentHP = maxHP_Boss;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Player takes damage
        }
    }

    private void Update()
    {
        
    }

}
