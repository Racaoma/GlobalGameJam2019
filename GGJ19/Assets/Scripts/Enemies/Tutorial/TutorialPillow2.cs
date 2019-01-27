using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPillow2 : Enemy
{
    //Methods
    private void Start()
    {
        base.currentHP = 1;
        animatorRef.Play("Idle");
    }
}
