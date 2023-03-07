using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField][Range (2,10)] float _walkSpeed;
    [SerializeField][Range(10, 20)] float _sprintSpeed;
    [SerializeField][Range(2f,15)] float _jumpHeight;
    [SerializeField][Range(1,7)] float _gravityScale;

        
    [Header("GroundCheck")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;

    float _movementSpeed;
    const float _gravity = -9.81f;
    bool _isGrounded;
    Vector3 velocity;

    CharacterController _controller;
    PcInput _input;


    private void Awake()
    {
        _controller= GetComponent<CharacterController>();
        _input = new PcInput();
    }
    private void Update()
    {
        //groundCheck
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        HandleMovement();
        HandleGravity();
    }

    void HandleMovement()
    {
        float vertical = _input.VerticalAxis;
        float horizontal = _input.HorizontalAxis;

        if (_input.Sprint)
            _movementSpeed = _sprintSpeed;
        else
            _movementSpeed = _walkSpeed;


        Vector3 direction = transform.right * horizontal + transform.forward * vertical;

        //diagonal speed limit
        if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) > 1.7f)
        {
            direction.Normalize();
        }

        _controller.Move(direction * _movementSpeed * Time.deltaTime);
    }
    void HandleGravity()
    {
        
        if (_isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (_input.Jump && _isGrounded)
        {
            velocity.y = Mathf.Sqrt(_jumpHeight * (-2f) * _gravity);
        }
        velocity.y += _gravity * Time.deltaTime * _gravityScale;
        _controller.Move(velocity * Time.deltaTime);
    }
    
}
