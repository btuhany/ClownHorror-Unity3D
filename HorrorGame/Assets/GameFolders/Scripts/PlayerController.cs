using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField][Range (2,10)] float _walkSpeed;
    [SerializeField][Range(10, 20)] float _sprintSpeed;
    [SerializeField][Range(2f,15)] float _jumpHeight;
    [SerializeField][Range(1,7)] float _gravityScale;

    float _movementSpeed;
    
    [Header("GroundCheck")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;
    
        
    CharacterController _controller;

    const float _gravity = -9.81f;

    public Vector3 velocity;

    bool _isGrounded;
    private void Awake()
    {
        _controller= GetComponent<CharacterController>();
    }
    private void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
            _movementSpeed = _sprintSpeed;
        else
            _movementSpeed = _walkSpeed;

        Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        _controller.Move(direction * _movementSpeed * Time.deltaTime);


        if (_isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        } 
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            velocity.y = Mathf.Sqrt(_jumpHeight * (-2f) * _gravity);

        }
        velocity.y += _gravity * Time.deltaTime * _gravityScale;

        _controller.Move(velocity * Time.deltaTime);
    }


    
}
