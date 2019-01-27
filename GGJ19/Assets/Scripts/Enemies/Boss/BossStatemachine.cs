using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatemachine : MonoBehaviour
{
    public Collider2D headCollider;
    public Animator animator;


    private BossState currentState;


    private void Start()
    {
        SetState(new IdleBoss(this));
    }

    private void Update()
    {
        currentState.Tick();
    }

    private void FixedUpdate()
    {
        currentState.FixedTick();
    }



    public void SetState(BossState state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }
}
