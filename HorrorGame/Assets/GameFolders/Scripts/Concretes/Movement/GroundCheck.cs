using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;

    bool _isGrounded;
    bool _isGroundedLastFrame;
    public bool IsGrounded { get => _isGrounded; }

    public event System.Action OnLanded;
    private void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (_isGrounded && !_isGroundedLastFrame)  //Check landing
            OnLanded?.Invoke();
        _isGroundedLastFrame = _isGrounded;
    }
}
