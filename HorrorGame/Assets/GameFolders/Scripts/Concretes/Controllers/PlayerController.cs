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

    [Header("Interact")]
    [SerializeField][Range(100f, 500f)] float _throwingForce;

    HeadBob _headbob;
    GroundCheck _groundCheck;
    CharacterControllerMovement _character;
    PcInput _input;
    PlayerSoundController _soundController;
    FlashlightController _flashLightController;
    PickUpDropObjMechanic _pickUpMechanic;
    Transform _transform;
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _character= GetComponent<CharacterControllerMovement>();
        _soundController= GetComponent<PlayerSoundController>();
        _headbob= GetComponent<HeadBob>();
        _groundCheck = GetComponent<GroundCheck>();
        _flashLightController= GetComponentInChildren<FlashlightController>();
        _pickUpMechanic = GetComponent<PickUpDropObjMechanic>();
        _input = new PcInput();
    }
    private void OnEnable()
    {
        _groundCheck.OnLanded += HandleOnLanded;
    }
    private void OnDisable()
    {
        _groundCheck.OnLanded -= HandleOnLanded;
    }
    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        Vector3 direction = _transform.right * _input.HorizontalAxis + _transform.forward * _input.VerticalAxis;
        if(direction == Vector3.zero)
        {
            _headbob.ResetPosition();
        }
        else if (_input.Sprint)
        {
            _character.GroundMovement(direction, _sprintSpeed);
            _headbob.RunningHeadBob();
            if(_groundCheck.IsGrounded)
                _soundController.PlayRunFootStep();
        }
        else
        {
            _character.GroundMovement(direction, _walkSpeed);
            _headbob.WalkingHeadBob();
            if (_groundCheck.IsGrounded)
                _soundController.PlayWalkFootStep();
        }
        if (_input.Jump)
        {
            _character.Jump(_jumpHeight);
            _soundController.PlayRunFootStep();
        }
        if(_input.Flashlight)
        {
            _flashLightController.Toggle();
            _soundController.PlayToggleLight();
        }
        if(_input.PickUpObj)
        {
            _pickUpMechanic.PickUpOrDrop();
        }
        if(_input.ThrowObj && _pickUpMechanic.IsHoldingObj)
        {
            _pickUpMechanic.ThrowObject(_throwingForce, direction);
        }
    }

    private void HandleOnLanded()
    {
        _soundController.PlayWalkFootStep();
    }

}
