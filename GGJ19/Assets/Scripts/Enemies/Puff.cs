using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class Puff : Enemy
{
    //Variables
    private int maxHP_Puff = 2;
    private float movementSpeed;
    private Vector3 movementDirection;
    private Collider2D collision;
    private float detectionRange;
    private LayerMask playerLayerMask = LayerMask.GetMask("Player");
    private CharacterController2D characterControllerRef;

    //Methods
    private void OnEnable()
    {
        base.currentHP = maxHP_Puff;
        characterControllerRef = this.GetComponent<CharacterController2D>();
        if (UnityEngine.Random.value >= 0.5f) movementDirection = Vector3.right;
        else movementDirection = Vector3.left;
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

    //TODO: Change Direction when hitting a wall
}
