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

        boss.leftFistCollider.enabled = false;
        boss.rightFistCollider.enabled = false;

        boss.SetAudio(null);
        boss.RandomState(Random.Range(4, 9));
    }
    
    public override void FixedTick()
    {

    }

    public override void Tick()
    {

    }

    public override void OnStateExit()
    {
        boss.leftFistCollider.enabled = true;
        boss.rightFistCollider.enabled = true;
    }
}
