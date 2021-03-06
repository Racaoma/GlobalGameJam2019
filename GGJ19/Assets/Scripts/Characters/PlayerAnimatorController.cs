﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerAnimatorController : MonoBehaviour
{
    public enum Weapon { Gun, Sword }
    Weapon currentWeapon = Weapon.Gun;
    private Weapon[] weaponLayers = {
        Weapon.Gun,
        Weapon.Sword,
        Weapon.Gun,
        Weapon.Sword
    };

    [SerializeField]
    private Animator _animator;


    public MaterialBlink _blink;


    [SerializeField]
    PlayerCharacterAnimationEvents _animationEvents;

    public Action OnExecuteAttack;

    private PlayerMovementController _movementController;
    private int _isWalkingAnimationKey;
    private int _isJumpingAnimationKey;
    private int _isFallingAnimationKey;
    private int _attackAnimationKey;
    private int _onKnockbackAnimationKey;
    private Coroutine _movementAnimationCoroutine;

  
    private void Awake()
    {
        _movementController = GetComponent<PlayerMovementController>();
        _animationEvents.OnExecuteAttack += OnAttack;
    }

    private void OnDestroy()
    {
        _animationEvents.OnExecuteAttack -= OnExecuteAttack;
    }

    private void Start()
    {
        _isWalkingAnimationKey = Animator.StringToHash("isWalking");
        _isJumpingAnimationKey = Animator.StringToHash("isJumping");
        _isFallingAnimationKey = Animator.StringToHash("isFalling");
        _attackAnimationKey = Animator.StringToHash("attack");
        _onKnockbackAnimationKey = Animator.StringToHash("onKnockback");
    }

    void LateUpdate()
    {
        UpdateMovementAnimation();
    }

    private void OnAttack()
    {
        OnExecuteAttack.SafeInvoke();
    }

    void UpdateMovementAnimation()
    {
        if (_movementController.enabled)
        {
            var state = _movementController.CurrentMovementState;
            _animator.SetBool(_isWalkingAnimationKey, state.IsRunning);
            _animator.SetBool(_isJumpingAnimationKey, state.IsJumping);
            _animator.SetBool(_isFallingAnimationKey, state.IsFalling);
        }
    }

    public void StartPlayingMovementAnimation()
    {
        if (_movementAnimationCoroutine != null)
        {
            StopCoroutine(_movementAnimationCoroutine);
        }
    }

    public void StopPlayingMovementAnimation()
    {
        if (_movementAnimationCoroutine != null)
        {
            StopCoroutine(_movementAnimationCoroutine);
        }
    }

    IEnumerator UpdateMovementAnimationCR()
    {
        while (true)
        {
            UpdateMovementAnimation();
            yield return null;
        }
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;

        // change animator layers
        for (int i = 0; i < weaponLayers.Length; i++)
        {
            _animator.SetLayerWeight(i + 1, weaponLayers[i] == currentWeapon ? 1 : 0);
        }
    }

    public void StartAttackAnimation()
    {
        
        _animator.SetTrigger(_attackAnimationKey);
    }

    public void StopAttackAnimation()
    {
        
    }

    public void StartKnockbackAnimation()
    {
        _animator.SetBool(_onKnockbackAnimationKey, true);
    }

    public void StopKnockbackAnimation()
    {
        _animator.SetBool(_onKnockbackAnimationKey, false);
    }
}
