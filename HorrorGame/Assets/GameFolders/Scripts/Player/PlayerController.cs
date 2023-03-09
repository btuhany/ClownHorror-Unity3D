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

    HeadBob _headbob;
    CharacterControllerMovement _character;
    PcInput _input;
    SoundController _soundController;
    FlashlightController _flashLightController;
    private void Awake()
    {
        _character= GetComponent<CharacterControllerMovement>();
        _soundController= GetComponent<SoundController>();
        _headbob= GetComponent<HeadBob>();
        _flashLightController= GetComponentInChildren<FlashlightController>();
        _input = new PcInput();
    }
    private void Update()
    {
        HandleInput();

    }

    void HandleInput()
    {
        Vector3 direction = transform.right * _input.HorizontalAxis + transform.forward * _input.VerticalAxis;
        if(direction == Vector3.zero)
        {
            _headbob.ResetPosition();
        }
        else if (_input.Sprint)
        {
            _character.GroundMovement(direction, _sprintSpeed);
            _soundController.PlayRunFootStep();
            _headbob.RunningHeadBob();
        }
        else
        {
            _character.GroundMovement(direction, _walkSpeed);
            _soundController.PlayWalkFootStep();
            _headbob.WalkingHeadBob();
        }
        if (_input.Jump)
        {
            _character.Jump(_jumpHeight);
        }
        if(_input.Flashlight)
        {
            _flashLightController.Toggle();
        }

    }
    
}
