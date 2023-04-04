using Controllers;
using UnityEngine;
using Inputs;
using Movements;
using System;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {

        [Header("Movement")]
        [SerializeField][Range(2, 10)] float _walkSpeed;
        [SerializeField][Range(0.5f, 5)] float _crouchSpeed;
        [SerializeField][Range(10, 20)] float _sprintSpeed;
        [SerializeField][Range(2f, 15)] float _jumpHeight;


        [Header("Interact")]
        [SerializeField][Range(100f, 2000f)] float _throwingForce;

        
        ArmsAnimationController _anim;
        PlayerHealthController _health;
        GunController _gunController;
        RaycasterController _raycaster;
        HeadBobController _headbob;
        GroundCheck _groundCheck;
        CharacterControllerMovement _characterMovement;
        PcInput _input;
        PlayerSoundController _soundController;
        FlashlightController _flashLightController;
        PickedUpObjectController _pickedUpController;
        Transform _transform;

        private void Awake()
        {
            _anim = GetComponentInChildren<ArmsAnimationController>();
            _transform = GetComponent<Transform>();
            _gunController = GetComponentInChildren<GunController>();
            _characterMovement = GetComponent<CharacterControllerMovement>();
            _soundController = GetComponent<PlayerSoundController>();
            _headbob = GetComponent<HeadBobController>();
            _groundCheck = GetComponent<GroundCheck>();
            _flashLightController = GetComponentInChildren<FlashlightController>();
            _raycaster = GetComponent<RaycasterController>();
            _pickedUpController = GetComponent<PickedUpObjectController>();
            _health = GetComponent<PlayerHealthController>();
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
            if(_health.IsDead) return;
            HandleInput();
        }

        void HandleInput()
        {

            Vector3 direction = _transform.right * _input.HorizontalAxis + _transform.forward * _input.VerticalAxis;
            if (direction == Vector3.zero)
            {
               _headbob.ResetPosition();
                _anim.Running(false);
            }
            else if (_input.Sprint && !_input.Aim)   //allows sprinting while crouching
            {
                if (_characterMovement.IsCrouched) { _characterMovement.StandUp(); }
                _characterMovement.GroundMovement(direction, _sprintSpeed);
               _headbob.RunningHeadBob();
                _anim.Running(true);
                if (_groundCheck.IsGrounded && direction.magnitude > 0.9f)
                    _soundController.PlayRunFootStep();

            }
            else if (_characterMovement.IsCrouched)
            {
                _characterMovement.GroundMovement(direction, _crouchSpeed);
                _anim.Running(false);
            }
            else
            {
                _anim.Running(false);
                _characterMovement.GroundMovement(direction, _walkSpeed);
               _headbob.WalkingHeadBob();
                if (_groundCheck.IsGrounded && direction.magnitude > 0.9f)
                    _soundController.PlayWalkFootStep();

            }

            if (_input.Jump && _groundCheck.IsGrounded)
            {
                if (_characterMovement.IsCrouched) { _characterMovement.StandUp(); }
                _characterMovement.Jump(_jumpHeight);
                _anim.Jumped();
                _soundController.PlayJump();
                _soundController.PlayRunFootStep();
            }
            if (_input.Flashlight)
            {
                _flashLightController.Toggle();
                _soundController.PlayToggleLight();
            }
            if (_input.Crouch && !_input.Sprint)
            {
                if (_characterMovement.IsCrouched)
                {
                    _characterMovement.StandUp();
                }
                else
                {
                    _soundController.PlayCrouch();
                    _characterMovement.Crouch();
                }
            } 
            if (_input.Fire && !_pickedUpController.IsThereObj )
            {
                if(_gunController.IsShootable)
                {
                    _anim.Shooted();
                    _gunController.Shoot();
                }
                else if(!PlayerInventoryManager.Instance.IsThereAmmo)
                {
                    _anim.NoAmmo();
                }


            }
            
            if (_input.Aim)
            {
                _gunController.AimCam();
                _anim.Aimed(true);
                
                _soundController.PlayBreathAimed();
                _pickedUpController.ReleaseIfThereIsObject();
            }
            else 
            {
                
                _gunController.DefaultCam();
                _soundController.StopBreathSound();
                _anim.Aimed(false);
              
            }
            if (_input.ThrowObj && _pickedUpController.IsThereObj)
            {
                _pickedUpController.ThrowObject(_throwingForce, direction);
            }
            if (_input.Interact && !_gunController.OnTransitionToAimCam)
            {
                _raycaster.InteractOrPickUp();
            }

        }

        private void HandleOnLanded()
        {
            _soundController.PlayWalkFootStep();
        }


    }
}


