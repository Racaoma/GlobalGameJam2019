﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendingBoss : BossState
{
    public DefendingBoss(BossStatemachine boss) : base(boss)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Boss Defending");

        boss.headCollider.enabled = false;
        boss.animator.SetBool("covering", true);

        boss.SetAudio(boss.sound_defending);
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
        boss.headCollider.enabled = true;
        boss.animator.SetBool("covering", false);
    }
}
