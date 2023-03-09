using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    [Header("Sway Controls")]
    [SerializeField] private float _smoothness;
    [SerializeField] private float _xMultiplier;
    [SerializeField] private float _yMultiplier;

    Light flashLight;
    
    private void Awake()
    {
        flashLight = GetComponent<Light>();
        flashLight.enabled = false;
    }
    private void LateUpdate()
    {
        if(flashLight.isActiveAndEnabled)
        {
            HandleSway();
        }   
    }
    private void HandleSway()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * _xMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * _yMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationY * rotationX;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, _smoothness * Time.deltaTime);
    }
    public void Toggle()
    {
        flashLight.enabled = !flashLight.isActiveAndEnabled;
    }
}
