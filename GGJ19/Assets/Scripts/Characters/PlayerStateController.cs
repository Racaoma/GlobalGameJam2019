﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(PlayerAnimatorController))]
[RequireComponent(typeof(PlayerAttackController))]
public class PlayerStateController : MonoBehaviour
{
    [SerializeField]

    public PlayerStateMachine StateMachine;
    public PlayerMovementController MovementController { get; private set; }
    public PlayerAnimatorController AnimationController { get; private set; }
    public PlayerAttackController PlayerAttackController { get; private set; }
    public Prime31.CharacterController2D CharacterController { get; private set; }

    public Vector2 StunKnockbackDirection { get; private set; }
    public float KnockbackDeaceleration = 60;
    public float KnockbackForce = 30;
    public float KnockbackDuration = 0.5f;
    public float StunDuration = 0.5f;
    public float invulneraabilityTime;
    public bool isVulnerable {get; private set;}

    public FreeMovementState FreeMovementState = new FreeMovementState();
    public StunState StunState = new StunState();

    private void Awake()
    {
        MovementController = GetComponent<PlayerMovementController>();
        AnimationController = GetComponent<PlayerAnimatorController>();
        PlayerAttackController = GetComponent<PlayerAttackController>();
        CharacterController = GetComponent<Prime31.CharacterController2D>();
        isVulnerable = true;
    }

    private void Start()
    {
        StateMachine = new PlayerStateMachine();
        StateMachine.Player = this;
        StateMachine.SetState(FreeMovementState);
    }

    public bool TakeHit(Vector3 hitterPosition)
    {
        StunKnockbackDirection = (transform.position - hitterPosition).normalized;

        if (isVulnerable)
        {
            if (StateMachine.CurrentState == FreeMovementState)
            {
                LudicController.Instance.DecreaseLudicMeter();
                StateMachine.SetState(StunState);
                isVulnerable = false;
                StartCoroutine(getVulnerable(StunDuration + invulneraabilityTime));
                return true;
            }
        }

        return false;

    }

    public IEnumerator getVulnerable(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        isVulnerable = true;
    }
}
