using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class Enemy_Puff : Enemy
{
    //Variables
    [SerializeField]
    private int maxHP_Puff;
    private Vector3 movementDirection;
    private Collider2D collision;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float detectionRange;
    private int playerLayer;
    private int groundLayer;
    private LayerMask playerLayerMask;
    private CharacterController2D characterControllerRef;

    //Methods
    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("Ground");
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
        if (collision.gameObject.layer == playerLayer)
        {
            //Player takes damage
        }
        else if (collision.gameObject.layer == groundLayer)
        {
            movementDirection *= -1;
        }
    }

    private void Update()
    {
        collision = Physics2D.OverlapCircle(this.transform.position, detectionRange, playerLayerMask);
        if(collision != null)
        {
            Vector3 chasingDirection = (collision.transform.position - this.transform.position);
            chasingDirection.y = 0;
            chasingDirection.Normalize();
            characterControllerRef.move(chasingDirection * movementSpeed);
        }
        else
        {
            characterControllerRef.move(movementDirection * movementSpeed);
        }
    }
}
