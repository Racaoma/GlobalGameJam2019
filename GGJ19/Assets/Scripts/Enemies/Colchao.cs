using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colchao : Enemy
{
    //Variables
    private int maxHP_Colchao = 3;
    private float shotRatio = 2f;
    private float currentShotTimer;

    //Methods
    private void OnEnable()
    {
        base.currentHP = maxHP_Colchao;
    }

    private void Update()
    {
        currentShotTimer -= Time.deltaTime;
        if (currentShotTimer <= 0f)
        {
            currentShotTimer = shotRatio;
        }
    }
}
