using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBoss : BossState
{
    public IdleBoss(BossStatemachine boss) : base(boss)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Boss Idle");
    }

    public override void FixedTick()
    {

    }

    public override void Tick()
    {

    }

    public override void OnStateExit()
    {
        
    }
}
