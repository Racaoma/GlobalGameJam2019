using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
[RequireComponent(typeof(PlayerAnimatorController))]
public class PlayerStateController : MonoBehaviour
{
    private PlayerStateMachine _stateMachine;
    public PlayerMovementController MovementController { get; private set; }
    public PlayerAnimatorController AnimationController { get; private set; }
    private FreeMovementState _freeMovementState = new FreeMovementState();

    private void Awake()
    {
        MovementController = GetComponent<PlayerMovementController>();
        AnimationController = GetComponent<PlayerAnimatorController>();
    }
    private void Start()
    {
        _stateMachine = new PlayerStateMachine();
        _stateMachine.Player = this;
        _stateMachine.SetState(_freeMovementState);
    }
}
