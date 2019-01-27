using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingBoss : BossState
{
    public AttackingBoss(BossStatemachine boss) : base(boss)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Boss Attacking");

        boss.animator.SetTrigger("leftAttack");
        boss.animator.SetTrigger("rightAttack");

        boss.IdleState(1);
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
