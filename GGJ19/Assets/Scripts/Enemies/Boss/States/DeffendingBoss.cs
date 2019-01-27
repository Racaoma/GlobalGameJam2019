using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeffendingBoss : BossState
{
    public DeffendingBoss(BossStatemachine boss) : base(boss)
    {
    }

    public override void OnStateEnter()
    {
        boss.headCollider.enabled = false;
    }

    public override void FixedTick()
    {

    }

    public override void Tick()
    {

    }

    public override void OnStateExit()
    {
        boss.headCollider.enabled = true;
    }
}
