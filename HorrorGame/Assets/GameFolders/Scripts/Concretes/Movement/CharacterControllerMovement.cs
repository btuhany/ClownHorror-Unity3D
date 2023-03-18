using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class CharacterControllerMovement : MonoBehaviour
{
    [SerializeField][Range(1, 7)] float _gravityScale;
    [SerializeField][Range(0.1f, 5)] float _forceToMoveObjects;

    const float _gravity = -9.81f;


    Vector3 velocity;

    GroundCheck _groundCheck;
    CharacterController _controller;
    Transform _transform;

    public Vector3 Velocity { get => velocity; }

    private void Awake()
    {
        _groundCheck = GetComponent<GroundCheck>();
        _controller = GetComponent<CharacterController>();
        _transform = transform;
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
    {                                           //for throwing object
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

    //Physic interaction with PickUpDrop layer objects
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.layer != 9)  //if gameobject's layer is PickUpDrop
            return;

        GameObject gameObj = hit.gameObject;
        Vector3 forceDir = gameObj.transform.position - _transform.position;
        forceDir.y = 0;
        forceDir.Normalize();

        Rigidbody gameObjRb = gameObj.GetComponent<Rigidbody>();
        gameObjRb.AddForceAtPosition(forceDir * _forceToMoveObjects , _transform.position, ForceMode.Impulse);


    }
}
