using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayHandler : MonoBehaviour
{
    [SerializeField] Transform _cameraSwayTransform;
    [Header("Default Weapon Sway")]
    [SerializeField] private float _weaponSwayA = 1;
    [SerializeField] float _weaponSwayB = 2;
    [SerializeField] float _weaponSwayScale = 600;
    [SerializeField] float _weaponSwayLerpSpeed = 14;
    [SerializeField] float _weaponSwayTime;
    [SerializeField] Vector3 _weaponSwayPos;

    [Header("Aimed Weapon Sway")]
    [SerializeField] private float _aimSwayA = 1;
    [SerializeField] float _aimSwayB = 2;
    [SerializeField] float _aimSwayScale = 600;
    [SerializeField] float _aimSwayLerpSpeed = 14;
    [SerializeField] float _aimSwayTime;
    [SerializeField] Vector3 _aimSwayPos;

    public float WeaponSwayTime { get => _weaponSwayTime; set => _weaponSwayTime = value; }

    public void WeaponSway()
    {
        Vector3 targetPos = LissajousCurve(_weaponSwayTime, _weaponSwayA, _weaponSwayB) / _weaponSwayScale;
        _weaponSwayPos = Vector3.Lerp(_weaponSwayPos, targetPos, Time.smoothDeltaTime * _weaponSwayLerpSpeed);
        _weaponSwayTime += Time.deltaTime;
        if (_weaponSwayTime > 6.3f)
        {
            _weaponSwayTime = 0;
        }
        transform.localPosition = _weaponSwayPos;
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
    private Vector3 LissajousCurve(float Time, float A, float B)
    {
        return new Vector3(Mathf.Sin(Time), A * Mathf.Sin(B * Time + Mathf.PI));
    }
}
