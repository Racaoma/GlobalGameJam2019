using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovementController : MonoBehaviour
{
    public MovementState CurrentMovementState { get; set; }
    [SerializeField]
    private float _maxMovementVelocity = 40;

    [SerializeField]
    private float _movementAcceleration = 80;

    [SerializeField]
    private float _oppositeDeaccelerationRatio = 1.5f;

    [SerializeField]
    private float _maxJumpForce = 20.0f;

    [SerializeField]
    private float _minJumpForce = 4.0f;

    [SerializeField]
    private float _canJumpVelocityTreshold = 15.0f;

    [SerializeField]
    private float _gravityAcceleration = 30.0f;

    [SerializeField]
    private float _pressDownAccelerationRatio = 3.0f;

    [SerializeField]
    private float _airAccelerationRatio = 0.5f;

    private CharacterController2D _characterController;
    private Vector2 _velocity;
    private float _gravityVelocity;
    private PlayerInput _input = new PlayerInput();
    private bool _canJump = false;
    private bool _isJumping = false;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController2D>();
        CurrentMovementState = new MovementState();

    }

    void Update()
    {
        if(!enabled)
        {
            return;
        }
        _input.Update();

        var acceleration = _movementAcceleration;
        if(_input.Direction.x != 0 && _velocity.x != 0)
        {
            if (Mathf.Sign(_input.Direction.x) != Mathf.Sign(_velocity.x))
            {
                acceleration *= _oppositeDeaccelerationRatio;
            }
        }

        if (_characterController.isGrounded)
        {
            _canJump = true;
            _velocity.y = 0;
            if(_input.IsDescending)
            {
                _characterController.ignoreOneWayPlatformsThisFrame = true;
            }
        }
        else
        {
            if (_velocity.y < _canJumpVelocityTreshold)
            {
                _canJump = false;
            }
            acceleration *= _airAccelerationRatio;
        }
        
        float gravityAcceleration = _gravityAcceleration;

        if(Input.GetAxisRaw("Vertical") < -0.5f)
        {
            gravityAcceleration *= _pressDownAccelerationRatio;
        }

        _velocity.x = Mathf.MoveTowards(_velocity.x, _input.Direction.x * _maxMovementVelocity, acceleration * Time.deltaTime);
        _velocity.y += gravityAcceleration * Time.deltaTime;
        
        if (Input.GetButton("Jump") || Input.GetAxisRaw("Vertical") > 0.5f)
        {
            if (_canJump)
            {
                GameEvents.PlayerAction.Jump.SafeInvoke();
                StartJump();
            }
        }
        else if (_isJumping)
        {
            StopJump();
        }

        Vector2 deltaMovement = _velocity * Time.deltaTime;
        _characterController.move(deltaMovement);
        _velocity = _characterController.velocity;
        if (_input.Direction.x > 0)
        {
            var scale = _characterController.transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        if (_input.Direction.x < 0)
        {
            var scale = _characterController.transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        CurrentMovementState.IsRunning = Mathf.Abs(_input.Direction.x) > 0;
        CurrentMovementState.IsJumping = _velocity.y > 0 && !_characterController.isGrounded;
        CurrentMovementState.IsFalling = _velocity.y <= 0 && (!_characterController.isGrounded);
    }

    public void StartJump()
    {
        _isJumping = true;
        _canJump = false;
        _velocity.y = _maxJumpForce;
    }

    public void StopJump()
    {
        _isJumping = false;
        _velocity.y = Mathf.Min(_velocity.y, _minJumpForce);
    }

    private class PlayerInput
    {
        public Vector2 Direction { get; set; }
        public bool IsJumping { get; set; }
        public bool IsDescending { get; set; }
        public void Update()
        {
            Direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            IsJumping = Input.GetAxisRaw("Vertical") > 0.5f || Input.GetButton("Jump");
            IsDescending = Input.GetAxisRaw("Vertical") < -0.5f;
        }
    }

    [System.Serializable]
    public class MovementState
    {
        public bool IsJumping { get; set; }
        public bool IsFalling { get; set; }
        public bool IsRunning { get; set; }
    }
}
