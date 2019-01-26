using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Almofada : Enemy
{
    //Variables
    private int maxHP_Almofada = 1;

    //Methods
    private void OnEnable()
    {
        base.currentHP = maxHP_Almofada;
    }

    private void Update()
    {
        
    }
}
