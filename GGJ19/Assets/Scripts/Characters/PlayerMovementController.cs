using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovementController : MonoBehaviour
{
    CharacterController2D characterController;
    public Vector2 inputVelocity;
    private Vector2 _velocity;
    private float _gravityVelocity;
    private Vector2 _movementInput = new Vector2();

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

    private bool _canJump = false;
    private bool _isJumping = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
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

        if (characterController.isGrounded)
        {
            _canJump = true;
            _gravityVelocity = 0;
        }
        else
        {
            if (_velocity.y < _canJumpVelocityTreshold)
            {
                _canJump = false;
            }
            acceleration *= _airAccelerationRatio;
        }

        _velocity.x = Mathf.MoveTowards(_velocity.x, _movementInput.x * _maxMovementVelocity, acceleration * Time.deltaTime);

        if (characterController.isGrounded)
        {
            _canJump = true;
            _gravityVelocity = 0;
        }
        else if (_velocity.y < _canJumpVelocityTreshold)
        {
            _canJump = false;
        }

        float gravityAcceleration = _gravityAcceleration;

        if(Input.GetAxisRaw("Vertical") < -0.5f)
        {
            gravityAcceleration *= _pressDownAccelerationRatio;
        }

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
        characterController.move(deltaMovement);
        _velocity = characterController.velocity;
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
