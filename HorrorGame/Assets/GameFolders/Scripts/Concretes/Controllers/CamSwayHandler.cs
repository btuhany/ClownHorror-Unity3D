using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwayHandler : MonoBehaviour
{
    [Header("Default Arms Sway")]    //this transform
    [SerializeField] private float _armSwayA = 6;
    [SerializeField] float _armsSwayB = 2;
    [SerializeField] float _armsSwayScale = 300;
    [SerializeField] float _armsSwayLerpSpeed = 14;
    Vector3 _armsSwayPos;
    float _armsSwayTime;
    float _lastArmsSwayTime; //using it on cam transitions
    Vector3 _lastArmsSwayPos;

    [Header("Aimed Weapon Sway")]   //camera transform
    [SerializeField] Transform _cameraSwayTransform;
    [SerializeField] private float _aimSwayA = 1;
    [SerializeField] float _aimSwayB = 2;
    [SerializeField] float _aimSwayScale = 600;
    [SerializeField] float _aimSwayLerpSpeed = 14;
    float _aimSwayTime;
    Vector3 _aimSwayPos;

    public float WeaponSwayTime { get => _armsSwayTime; set => _armsSwayTime = value; }

    public void WeaponSway()
    {
     
        Vector3 targetPos = LissajousCurve(_armsSwayTime, _armSwayA, _armsSwayB) / _armsSwayScale;
        _armsSwayPos = Vector3.Lerp(_armsSwayPos, targetPos, Time.smoothDeltaTime * _armsSwayLerpSpeed);
        _armsSwayTime += Time.deltaTime;
        if (_armsSwayTime > 6.3f)
        {
            _armsSwayTime = 0;
        }
        transform.localPosition = _armsSwayPos;
    }
    public void AimSway()
    {
        Vector3 targetPos = LissajousCurve(_aimSwayTime, _aimSwayA, _aimSwayB) / _aimSwayScale;
        _aimSwayPos = Vector3.Lerp(_aimSwayPos, targetPos, Time.smoothDeltaTime * _aimSwayLerpSpeed);
        _aimSwayTime += Time.deltaTime;
        if (_aimSwayTime > 6.3f)
        {
            _aimSwayTime = 0;
        }
        _cameraSwayTransform.localPosition = _aimSwayPos;
    }
    public void OnAimCamTransition()
    {
        if (transform.localPosition == Vector3.zero)
            return;
        
        _lastArmsSwayPos = transform.localPosition;
        _lastArmsSwayTime = _armsSwayTime;
        transform.localPosition = Vector3.zero;
        _armsSwayTime = 0;
    }
    public void OnDefaultCamTransition()
    {  
        transform.localPosition = _lastArmsSwayPos;
        _armsSwayTime = _lastArmsSwayTime;
    }
    private Vector3 LissajousCurve(float Time, float A, float B)
    {
        return new Vector3(Mathf.Sin(Time), A * Mathf.Sin(B * Time + Mathf.PI));
    }
}
