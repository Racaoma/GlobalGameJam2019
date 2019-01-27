using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingFloorBoss : BossState
{
    public HittingFloorBoss(BossStatemachine boss) : base(boss)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Boss HittingFloor");

        boss.animator.SetBool("hittingFloor", true);

        boss.IdleState(Random.Range(2, 6));
    }

    public override void FixedTick()
    {

    }

    public override void Tick()
    {
        
    }

    public override void OnStateExit()
    {
        boss.animator.SetBool("hittingFloor", false);
    }
}
