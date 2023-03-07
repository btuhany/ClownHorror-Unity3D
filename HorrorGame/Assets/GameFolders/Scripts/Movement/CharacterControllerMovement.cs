using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Windows;

public class CharacterControllerMovement : MonoBehaviour
{
    [SerializeField][Range(1, 7)] float _gravityScale;

    [Header("GroundCheck")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;

    const float _gravity = -9.81f;
    bool _isGrounded;
    Vector3 velocity;


    CharacterController _controller;

    public bool IsGrounded { get => _isGrounded;}

    private void Awake()
    {
        _controller= GetComponent<CharacterController>();
    }
    private void Update()
    {
        //groundCheck
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        HandleGravity();
    }
    private void HandleGravity()
    {
        if (_isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
 
        velocity.y += _gravity * Time.deltaTime * _gravityScale;
        _controller.Move(velocity * Time.deltaTime);
    }

    public void GroundMovement(Vector3 direction, float speed)
    {
        
        //diagonal speed limit
        if (direction.magnitude > 1.7f)
        {
            direction.Normalize();
        }

        _controller.Move(direction * speed * Time.deltaTime);
    }
    public void Jump(float jumpHeight)
    {
        if (!_isGrounded) return;
        velocity.y = Mathf.Sqrt(jumpHeight * (-2f) * _gravity);

    }
}
