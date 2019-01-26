using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class Enemy_Pillow : Enemy
{
    //Variables
    private int maxHP_Pillow = 1;
    private Vector3 movementDirection;
    private CharacterController2D characterControllerRef;

    //Methods
    private void Start()
    {
        characterControllerRef = this.GetComponent<CharacterController2D>();
    }

    private void OnEnable()
    {
        base.currentHP = maxHP_Pillow;
        if (UnityEngine.Random.value >= 0.5f) movementDirection = Vector3.right;
        else movementDirection = Vector3.left;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
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
        characterControllerRef.move(movementDirection);
    }
}
