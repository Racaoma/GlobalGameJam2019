using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovementState : PlayerState
{
    public override void OnEnter()
    {
        Player.MovementController.enabled = true;
        Player.AnimationController.StartPlayingMovementAnimation();
        Player.PlayerAttackController.enabled = true;
    }

    public override void OnExit()
    {
        Player.MovementController.enabled = false;
        Player.PlayerAttackController.enabled = false;
        Player.AnimationController.StopPlayingMovementAnimation();
    }
}
