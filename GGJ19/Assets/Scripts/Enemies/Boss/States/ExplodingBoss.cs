using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBoss : BossState
{
    public ExplodingBoss(BossStatemachine boss) : base(boss)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Boss Exploding");

        boss.animator.SetBool("exploding", true);
        boss.animator.SetTrigger("explode");

        boss.leftFistCollider.enabled = false;
        boss.rightFistCollider.enabled = false;

        boss.IdleState(Random.Range(4, 9));
    }

    public override void FixedTick()
    {

    }

    public override void Tick()
    {
        
    }

    public override void OnStateExit()
    {
        boss.animator.SetBool("exploding", false);

        boss.leftFistCollider.enabled = true;
        boss.rightFistCollider.enabled = true;
    }
}