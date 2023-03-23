using Controllers;
using UnityEngine;
using Inputs;
using Movements;

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

            _transform = GetComponent<Transform>();
            _gunController = GetComponentInChildren<GunController>();
            _characterMovement = GetComponent<CharacterControllerMovement>();
            _soundController = GetComponent<PlayerSoundController>();
            _headbob = GetComponent<HeadBobController>();
            _groundCheck = GetComponent<GroundCheck>();
            _flashLightController = GetComponentInChildren<FlashlightController>();
            _raycaster = GetComponent<RaycasterController>();
            _pickedUpController = GetComponent<PickedUpObjectController>();
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
            if (direction == Vector3.zero)
            {
                _headbob.ResetPosition();

            }
            else if (_input.Sprint)   //allows sprinting while crouching
            {
                if (_characterMovement.IsCrouched) { _characterMovement.StandUp(); }
                _characterMovement.GroundMovement(direction, _sprintSpeed);
                _headbob.RunningHeadBob();
                if (_groundCheck.IsGrounded && direction.magnitude > 0.9f)
                    _soundController.PlayRunFootStep();

            }
            else if (_characterMovement.IsCrouched)
            {
                _characterMovement.GroundMovement(direction, _crouchSpeed);
            }
            else
            {
                _characterMovement.GroundMovement(direction, _walkSpeed);
                _headbob.WalkingHeadBob();
                if (_groundCheck.IsGrounded && direction.magnitude > 0.9f)
                    _soundController.PlayWalkFootStep();

            }

            if (_input.Jump && _groundCheck.IsGrounded)
            {
                if (_characterMovement.IsCrouched) { _characterMovement.StandUp(); }
                _characterMovement.Jump(_jumpHeight);
                _soundController.PlayRunFootStep();
            }
            if (_input.Flashlight)
            {
                _flashLightController.Toggle();
                _soundController.PlayToggleLight();
            }
            if (_input.ThrowObj && _pickedUpController.IsThereObj)
            {
                _pickedUpController.ThrowObject(_throwingForce, direction);
            }
            if (_input.Interact)
            {

                _raycaster.InteractOrPickUp();
            }
            if (_input.Crouch && !_input.Sprint)
            {
                if (_characterMovement.IsCrouched)
                {
                    _characterMovement.StandUp();
                }
                else
                {
                    _characterMovement.Crouch();
                }
            }
            if (_pickedUpController.IsThereObj) return;
            if(_input.Fire)
            {
                _gunController.Shoot();
            }
            if(_input.Aim)
            {
                _gunController.AimCam();
            }
            else 
            {
                _gunController.DefaultCam();
            }
            
        }

        private void HandleOnLanded()
        {
            _soundController.PlayWalkFootStep();
        }


    }
}


