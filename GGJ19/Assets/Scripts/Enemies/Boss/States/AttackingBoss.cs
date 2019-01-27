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

        boss.SetAudio(boss.sound_attacking);
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

    }
}
