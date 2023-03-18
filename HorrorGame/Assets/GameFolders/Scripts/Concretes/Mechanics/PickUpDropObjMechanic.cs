using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDropObjMechanic : MonoBehaviour
{
    [SerializeField] float _objectGrabbedMovingSpeed;
    [SerializeField] Transform _playerCamera;
    [SerializeField] Transform _grabPoint;
    [SerializeField] LayerMask _layer;
    [SerializeField] float _pickUpDistance;

    BreakableObjectController _breakableObj;  
    Rigidbody _breakableObjRb;
    bool _isThereObj;

    public bool IsHoldingObj { get => _isThereObj;}


    public void PickUpOrDrop()
    {
        if (!_isThereObj &&  Physics.Raycast(_playerCamera.position, _playerCamera.forward, out RaycastHit hit, _pickUpDistance, _layer))
        {
            _breakableObj = hit.collider.GetComponent<BreakableObjectController>();
            _breakableObjRb = _breakableObj.Rb;
            GrabObject(_breakableObj);
        }
        else if(_isThereObj)
        {
            ReleaseObject();
        }
    }
    public void ThrowObject(float force, Vector3 dir)
    {
        if (!_isThereObj) return;
        
        _breakableObj.Throwed((_grabPoint.forward + dir / 3), force); //groundMovement vector affects throwing
        _isThereObj = false;
    }

    //private void Update()
    //{
    //    Debug.DrawRay(_playerCamera.position, _playerCamera.forward * _pickUpDistance, Color.red);
    //}

    private void LateUpdate()
    {
        if (_isThereObj)
        {
            Vector3 pos = _grabPoint.position - _breakableObj.transform.position;
            Vector3 newVelocity = pos * _objectGrabbedMovingSpeed * pos.magnitude;

            _breakableObjRb.MoveRotation(transform.rotation);
            _breakableObjRb.velocity = newVelocity;
        }
    }
    private void GrabObject(BreakableObjectController breakableObj)
    {
        breakableObj.Grabbed();
        _isThereObj = true;
    }
    private void ReleaseObject()
    {
        _breakableObj.Released();
        _isThereObj = false;

    }
}
