using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Windows;

public class CharacterControllerMovement : MonoBehaviour
{
    [SerializeField][Range(1, 7)] float _gravityScale;

    const float _gravity = -9.81f;

    Vector3 velocity;

    GroundCheck _groundCheck;
    CharacterController _controller;

    private void Awake()
    {
        _groundCheck = GetComponent<GroundCheck>();
        _controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        HandleGravity();
    }
    private void HandleGravity()
    {
        if (_groundCheck.IsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //else if(velocity.y < 0)
        //{
        //    _gravityScale *= 1.2f;
        //}
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
        if (!_groundCheck.IsGrounded) return;
        velocity.y = Mathf.Sqrt(jumpHeight * (-2f) * _gravity);

    }
}
