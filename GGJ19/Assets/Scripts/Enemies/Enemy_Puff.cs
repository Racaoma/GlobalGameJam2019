using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class Enemy_Puff : Enemy
{
    //Variables
    private int maxHP_Puff = 2;
    private Vector3 movementDirection;
    private Collider2D collision;
    private float detectionRange;
    private LayerMask playerLayerMask;
    private CharacterController2D characterControllerRef;

    //Methods
    private void Start()
    {
        playerLayerMask = LayerMask.GetMask("Player");
        characterControllerRef = this.GetComponent<CharacterController2D>();
    }

    private void OnEnable()
    {
        base.currentHP = maxHP_Puff;
        if (UnityEngine.Random.value >= 0.5f) movementDirection = Vector3.right;
        else movementDirection = Vector3.left;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Player takes damage
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            movementDirection *= -1;
        }
    }

    private void Update()
    {
        collision = Physics2D.OverlapCircle(this.transform.position, detectionRange, playerLayerMask);
        if(collision != null)
        {
            characterControllerRef.move(collision.transform.position - this.transform.position);
        }
        else
        {
            characterControllerRef.move(movementDirection);
        }
    }
}
