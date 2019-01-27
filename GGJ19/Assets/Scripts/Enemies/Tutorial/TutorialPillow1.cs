using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class TutorialPillow1 : Enemy
{
    //Methods
    private void Start()
    {
        base.currentHP = 1;
        animatorRef.Play("Idle");
    }
}
