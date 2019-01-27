using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatedBoss : BossState
{
    public DefeatedBoss(BossStatemachine boss) : base(boss)
    {
    }

    public override void OnStateEnter()
    {
        boss.leftFistCollider.enabled = false;
        boss.rightFistCollider.enabled = false;
        boss.headCollider.enabled = false;

        boss.SetAudio(boss.sound_defeated);
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
