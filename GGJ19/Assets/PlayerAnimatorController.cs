using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private PlayerMovementController _movementController;
    private int _isWalkingAnimationKey;
    private int _isJumpingAnimationKey;
    private int _isFallingAnimationKey;
    private Coroutine _movementAnimationCoroutine;
    private void Awake()
    {
        _movementController = GetComponent<PlayerMovementController>();
    }
    private void Start()
    {
        _isWalkingAnimationKey = Animator.StringToHash("isWalking");
        _isJumpingAnimationKey = Animator.StringToHash("isJumping");
        _isFallingAnimationKey = Animator.StringToHash("isFalling");
    }

    void LateUpdate()
    {
        UpdateMovementAnimation();
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
        if(_movementAnimationCoroutine != null)
        {
            StopCoroutine(_movementAnimationCoroutine);
        }
    }

    public void StopPlayingMovementAnimation()
    {
        if(_movementAnimationCoroutine != null)
        {
            StopCoroutine(_movementAnimationCoroutine);
        }
    }

    IEnumerator UpdateMovementAnimationCR()
    {
        while(true)
        {
            UpdateMovementAnimation();
            yield return null;
        }
    }
}
