using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] Transform _player;
    [Header("Sensitivity")]
    [SerializeField][Range(250f, 1000)] float _mouseXSensitivity;
    [SerializeField][Range(250f, 1000)] float _mouseYSensitivity;
    [Header("Vertical Clamp")]
    [SerializeField] float MinVerticalAngle;
    [SerializeField] float MaxVerticalAngle;
    

    float yRotation;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * _mouseYSensitivity * Time.deltaTime;
        float horizontal = Input.GetAxis("Mouse X") * _mouseXSensitivity * Time.deltaTime;

        yRotation -= vertical;
        yRotation = Mathf.Clamp(yRotation, MinVerticalAngle, MaxVerticalAngle);

        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        _player.Rotate(Vector3.up * horizontal);
    }
}
