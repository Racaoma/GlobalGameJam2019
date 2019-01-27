using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState
{
    protected BossStatemachine boss;

    public abstract void Tick();
    public abstract void FixedTick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public BossState(BossStatemachine boss)
    {
        this.boss = boss;
    }
}
