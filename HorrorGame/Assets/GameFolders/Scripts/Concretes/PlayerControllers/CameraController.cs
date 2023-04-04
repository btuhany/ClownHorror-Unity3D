
using UnityEngine;
namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform _player;
        [Header("Sensitivity")]
        [SerializeField][Range(50f, 350f)] float _mouseXSensitivity;
        [SerializeField][Range(50f, 350f)] float _mouseYSensitivity;
        [Header("Vertical Clamp")]
        [SerializeField] float MinVerticalAngle;
        [SerializeField] float MaxVerticalAngle;
        bool _disableInput;

        float _yRotation;
        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.Instance.OnGameOver += HandleOnGameOver;

        }
        private void HandleOnGameOver()
        {
            _disableInput= true;
        }
        private void LateUpdate()
        {
            if (_disableInput) return;
            float vertical = Input.GetAxis("Mouse Y") * _mouseYSensitivity * Time.fixedDeltaTime;
            float horizontal = Input.GetAxis("Mouse X") * _mouseXSensitivity * Time.fixedDeltaTime;

            _yRotation -= vertical;
            _yRotation = Mathf.Clamp(_yRotation, MinVerticalAngle, MaxVerticalAngle);

            transform.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);
            _player.Rotate(Vector3.up * horizontal);
        }
    }
}

