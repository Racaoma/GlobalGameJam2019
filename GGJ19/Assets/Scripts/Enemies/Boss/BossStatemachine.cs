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
        Debug.Log("Boss Idle");
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

    public void SetState(BossState state, float delay)
    {
        StartCoroutine(WaitToChange(state, delay));
    }

    IEnumerator WaitToChange(BossState state, float delay)
    {
        yield return new WaitForSeconds(delay);
        SetState(state);
    }
}
