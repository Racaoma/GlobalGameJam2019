using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovementController : MonoBehaviour
{
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
    private Vector2 _movementInput = new Vector2();
    private bool _canJump = false;
    private bool _isJumping = false;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        _movementInput.x = Input.GetAxis("Horizontal");
        _movementInput.y = Input.GetAxis("Vertical");

        var acceleration = _movementAcceleration;
        if(_movementInput.x != 0 && _velocity.x != 0)
        {
            if (Mathf.Sign(_movementInput.x) != Mathf.Sign(_velocity.x))
            {
                acceleration *= _oppositeDeaccelerationRatio;
            }
        }

        if (_characterController.isGrounded)
        {
            _canJump = true;
            _velocity.y = 0;
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

        _velocity.x = Mathf.MoveTowards(_velocity.x, _movementInput.x * _maxMovementVelocity, acceleration * Time.deltaTime);
        _velocity.y += gravityAcceleration * Time.deltaTime;
        
        if (Input.GetButton("Jump") || Input.GetAxisRaw("Vertical") > 0.5f)
        {
            if (_canJump)
            {
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
}
