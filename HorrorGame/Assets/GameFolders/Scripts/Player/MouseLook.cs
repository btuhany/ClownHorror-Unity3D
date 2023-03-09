using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform _player;
    [Header("Sensitivity")]
    [SerializeField][Range(50f, 350f)] float _mouseXSensitivity;
    [SerializeField][Range(50f, 350f)] float _mouseYSensitivity;
    [Header("Vertical Clamp")]
    [SerializeField] float MinVerticalAngle;
    [SerializeField] float MaxVerticalAngle;
    

    float _yRotation;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
       
    }
    private void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * _mouseYSensitivity * Time.deltaTime;
        float horizontal = Input.GetAxis("Mouse X") * _mouseXSensitivity * Time.deltaTime;

        _yRotation -= vertical;
        _yRotation = Mathf.Clamp(_yRotation, MinVerticalAngle, MaxVerticalAngle);

        transform.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);
        _player.Rotate(Vector3.up * horizontal);
    }
}
