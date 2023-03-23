using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GunController : MonoBehaviour
{
    [Header("FX Transforms")]
    [SerializeField] Transform _barrelTransform;
    [Header("Shooting")]
    [SerializeField] private float _range = 50f;
    [SerializeField] private Camera _fpsCam;
    [SerializeField] LayerMask _layerMask;

    [Header("Camera Movement At Aim")]
    [SerializeField] Transform _defaultGunPos;
    [SerializeField] Transform _aimingGunPos;
    [SerializeField] float _camFovAtAim;
    [SerializeField] float _gunAimPosLerpSpeed;
    [SerializeField] float _gunDefaultPosLerpSpeed;
    [SerializeField] float _aimCamFOVLerpSpeed;
    [SerializeField] float _defaultFOVCamLerpSpeed;


    private bool _onTransitionToAimCam;
    private bool _isOnDefaultCam;

    [SerializeField]  RecoilEffectHandler _recoil;

    Animator _anim;
    SwayHandler _swayHandler;
    private bool _cancelSway;
    float _defaultFov;

   

    private void Awake()
    {
        _anim= GetComponent<Animator>();
        _swayHandler = GetComponent<SwayHandler>();
        _defaultFov = _fpsCam.fieldOfView;
        transform.position = _defaultGunPos.position;
    }
    private void Update()
    {
        
            
        if(_onTransitionToAimCam)
        {
            _swayHandler.AimSway();
        }
        else if(_isOnDefaultCam)  //dont check onTransitionToDefaultCam because _swayHandler.WeaponSway also manipulates postion therefore prevents the transition.
        {
            _swayHandler.WeaponSway();
        }
            
    

           
        
    }
    public void Shoot()
    {

        _recoil.RecoilEffect();
        if (Physics.Raycast(_fpsCam.transform.position, _fpsCam.transform.forward, out RaycastHit hit, _range, _layerMask))
        {
            GameObject newObj = ObjectPoolManager.Instance.GetObjectFromPool(PoolObjectId.MuzzleFlashFx);
            newObj.transform.position = _barrelTransform.position;
            newObj.transform.rotation = _barrelTransform.rotation;
            newObj.SetActive(true);
        }
    }
    public void AimCam()
    {
        _isOnDefaultCam = false;
        if (Vector3.Distance(transform.position, _aimingGunPos.position) < 0.2f && Mathf.Abs(_camFovAtAim - _fpsCam.fieldOfView) < 0.02f) return;
        _onTransitionToAimCam = true;
        transform.position = Vector3.Lerp(transform.position, _aimingGunPos.position, Time.deltaTime * _gunAimPosLerpSpeed);
        _fpsCam.fieldOfView = Mathf.Lerp(_fpsCam.fieldOfView, _camFovAtAim, Time.deltaTime * _aimCamFOVLerpSpeed);
       
        
    }
    public void DefaultCam()
    {
        if (Vector3.Distance(transform.position, _defaultGunPos.position) < 0.03f && Mathf.Abs(_defaultFov - _fpsCam.fieldOfView) < 0.02f)
        {
            _isOnDefaultCam = true;
           
            return;
        }
        
        _onTransitionToAimCam = false;
        _swayHandler.WeaponSwayTime = 5.5f;
        
        transform.position = Vector3.Lerp(transform.position, _defaultGunPos.position, Time.deltaTime * _gunDefaultPosLerpSpeed);
        _fpsCam.fieldOfView = Mathf.Lerp(_fpsCam.fieldOfView, _defaultFov, Time.deltaTime * _defaultFOVCamLerpSpeed);
        
    }




    void CasingRelease()
    {

    }
    private void GunRecoil()
    {
        
    }
}
