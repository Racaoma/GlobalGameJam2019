using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class Almofada : Enemy
{
    //Variables
    private int maxHP_Almofada = 1;
    private Vector3 movementDirection;
    private CharacterController2D characterControllerRef;

    //Methods
    private void OnEnable()
    {
        base.currentHP = maxHP_Almofada;
        characterControllerRef = this.GetComponent<CharacterController2D>();
        if (UnityEngine.Random.value >= 0.5f) movementDirection = Vector3.right;
        else movementDirection = Vector3.left;
    }

    private void Update()
    {
        characterControllerRef.move(movementDirection);
    }

    //TODO: Change Direction when hitting a wall
}
