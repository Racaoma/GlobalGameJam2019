using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : PlayerState
{
    private Coroutine _timeoutRoutine;
    public override void OnEnter()
    {
        _timeoutRoutine = Player.StartCoroutine(TimeoutRoutine());
        Player.AnimationController.StartKnockbackAnimation();
    }

    public IEnumerator TimeoutRoutine()
    {
        float timeout = Player.KnockbackDuration;
        while (timeout > 0)
        {
            timeout -= Time.deltaTime;
            Player.CharacterController.move(Player.StunKnockbackDirection * Player.KnockbackForce * Time.deltaTime);

            yield return null;
        }
        yield return null;
        yield return new WaitForSeconds(Player.StunDuration);

        Player.StateMachine.SetState(Player.FreeMovementState);
    }

    public override void OnExit()
    {
        if(_timeoutRoutine != null)
        {
            Player.StopCoroutine(_timeoutRoutine);
        }
        Player.AnimationController.StopKnockbackAnimation();
    }
}
