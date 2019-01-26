using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puff : Enemy
{
    //Variables
    private int maxHP_Puff = 2;

    //Methods
    private void OnEnable()
    {
        base.currentHP = maxHP_Puff;
    }

    private void Update()
    {

    }
}
