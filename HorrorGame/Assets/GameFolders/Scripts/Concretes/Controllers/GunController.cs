using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GunController : MonoBehaviour
{
    [SerializeField] private float _range = 50f;
    [SerializeField] private Camera _fpsCam;
    [SerializeField] LayerMask _layerMask;

    [SerializeField] Transform _cameraHolder;

    [SerializeField]  RecoilEffectHandler _recoil;
    [SerializeField] Transform _defaultGunPos;
    [SerializeField] Transform _aimingGunPos;


    [SerializeField] private float _swayA = 1;
    [SerializeField] float _swayB = 2;
    [SerializeField] float _swayScale = 600;
    [SerializeField] float _swayLerpSpeed = 14;
    [SerializeField] float _swayTime;
    [SerializeField] Vector3 _swayPos;

     float _defaultFov;
    [SerializeField] float _aimFov;
    Animator _anim;
    private void Awake()
    {
        _anim= GetComponentInChildren<Animator>();
        _defaultFov = _fpsCam.fieldOfView;
      
    }
    private void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            _anim.SetTrigger("Fire");
            _recoil.RecoilEffect();
           

            
            Shoot();
        }
        if(Input.GetButton("Fire2"))
        {
            Aim();
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _defaultGunPos.position, Time.deltaTime * 5f);
            _fpsCam.fieldOfView = Mathf.Lerp(_fpsCam.fieldOfView, _defaultFov, Time.deltaTime * 5f);
            WeaponSway();
        }
                

    }
    private void Shoot()
    {
        
        if (Physics.Raycast(_fpsCam.transform.position, _fpsCam.transform.forward, out RaycastHit hit, _range, _layerMask))
        {
            
            
        }
    }
    private void Aim()
    {
        transform.position = Vector3.Lerp(transform.position, _aimingGunPos.position, Time.deltaTime * 5f);
        
        
            Vector3 targetPos = LissajousCurve(_swayTime, _swayA, _swayB) / _swayScale;
            _swayPos = Vector3.Lerp(_swayPos, targetPos, Time.smoothDeltaTime * _swayLerpSpeed);
            _swayTime += Time.deltaTime;
            if (_swayTime > 6.3f)
            {
                _swayTime = 0;
            }
            _cameraHolder.localPosition = _swayPos;
        
        _fpsCam.fieldOfView = Mathf.Lerp(_fpsCam.fieldOfView, _aimFov, Time.deltaTime * 5f);
    }
    private void WeaponSway()
    {
        Vector3 targetPos = LissajousCurve(_swayTime,_swayA,_swayB) / _swayScale;
        _swayPos = Vector3.Lerp(_swayPos, targetPos, Time.smoothDeltaTime * _swayLerpSpeed);
        _swayTime += Time.deltaTime;
        if(_swayTime>6.3f)
        {
            _swayTime = 0;
        }
        transform.localPosition = _swayPos;
    }
    private Vector3 LissajousCurve(float Time, float A, float B)
    {
        return new Vector3(Mathf.Sin(Time), A * Mathf.Sin(B * Time + Mathf.PI));
    }

    void CasingRelease()
    {

    }
    private void GunRecoil()
    {
        
    }
}
