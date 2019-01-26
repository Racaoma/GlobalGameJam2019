using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class Enemy_Pillow : Enemy
{
    //Variables
    [SerializeField]
    private int maxHP_Pillow;
    private Vector3 movementDirection;
    private CharacterController2D characterControllerRef;
    [SerializeField]
    private float movementSpeed;
    private int playerLayer;
    private int groundLayer;

    //Methods
    private void Start()
    {
        characterControllerRef = this.GetComponent<CharacterController2D>();
        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    private void OnEnable()
    {
        base.currentHP = maxHP_Pillow;
        if (UnityEngine.Random.value >= 0.5f) movementDirection = Vector3.right;
        else movementDirection = Vector3.left;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == playerLayer)
        {
            LudicController.Instance.ludicMeter--;
            //Player stun
        }
        else if (collision.gameObject.layer == groundLayer)
        {
            movementDirection *= -1;
        }
    }

    private void Update()
    {
        characterControllerRef.move(movementDirection * movementSpeed);
    }
}
