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
    private CharacterController2D characterControllerRef;

    //Methods
    private void Start()
    {
        characterControllerRef = this.GetComponent<CharacterController2D>();
    }

    private void OnEnable()
    {
        base.currentHP = maxHP_Puff;
        currentState = enemyState.Active;
        if (UnityEngine.Random.value >= 0.5f) movementDirection = Vector3.right;
        else movementDirection = Vector3.left;
        animatorRef.enabled = true;
        spriteRendererRef.sortingOrder = 0;
        boxCollider2DRef.enabled = true;
        rigidBody2DRef.isKinematic = false;
        animatorRef.Play("Idle");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            var player = collision.gameObject.GetComponent<PlayerStateController>();
            if (player != null)
            {
                player.TakeHit(transform.position);
            }
        }
        else if (collision.gameObject.layer == groundLayer)
        {
            movementDirection *= -1;
        }
    }

    private void move(Vector3 movementTarget)
    {
        if (movementDirection.x >= 0f) this.transform.GetChild(0).localScale = new Vector3(-1f, 1f, 1f);
        else this.transform.GetChild(0).localScale = Vector3.one;

        characterControllerRef.move(movementTarget * movementSpeed);
        animatorRef.SetBool("isWalking", true);
    }

    private void Update()
    {
        if (currentState == enemyState.Stunned && stunTimer >= 0f)
        {
            stunTimer -= Time.deltaTime;
            animatorRef.SetBool("isWalking", false);
        }
        else
        {
            currentState = enemyState.Active;
            collision = Physics2D.OverlapCircle(this.transform.position, detectionRange, playerLayerMask);
            if (collision != null)
            {
                Vector3 chasingDirection = (collision.transform.position - this.transform.position);
                chasingDirection.y = 0;
                chasingDirection.Normalize();
                move(chasingDirection);
            }
            else
            {
                move(movementDirection);
            }
        }
    }
}
