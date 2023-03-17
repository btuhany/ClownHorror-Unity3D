using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDropObjMechanic : MonoBehaviour
{
    [SerializeField] float _objectMovingSpeed;
    [SerializeField] Transform _playerCamera;
    [SerializeField] Transform _grabPoint;
    [SerializeField] LayerMask _layer;
    [SerializeField] float _pickUpDistance;

    GameObject _grabbedObj;  
    Rigidbody _grabbedObjRb;
    bool _isThereObj;

    public bool IsHoldingObj { get => _isThereObj;}


    public void PickUpOrDrop()
    {
        if (!_isThereObj &&  Physics.Raycast(_playerCamera.position, _playerCamera.forward, out RaycastHit hit, _pickUpDistance, _layer))
        {
            GrabObject(hit.collider.gameObject);
        }
        else if(_isThereObj)
        {
            ReleaseObject();
        }
    }
    public void ThrowObject(float force, Vector3 dir)
    {
        if (!_isThereObj) return;
        ReleaseObject();
        _grabbedObjRb.AddForce((_grabPoint.forward + dir / 3) * force);  //groundMovement vector affects throwing
    }

    //private void Update()
    //{
    //    Debug.DrawRay(_playerCamera.position, _playerCamera.forward * _pickUpDistance, Color.red);
    //}

    private void LateUpdate()
    {
        if (_isThereObj)
        {
            Vector3 pos = _grabPoint.position - _grabbedObj.transform.position;
            _grabbedObjRb.MoveRotation(transform.rotation);
            _grabbedObjRb.velocity = pos * _objectMovingSpeed * pos.magnitude;
        }
    }
    private void GrabObject(GameObject gameObj)
    {
        
        _grabbedObj = gameObj;
        _grabbedObjRb = _grabbedObj.GetComponent<Rigidbody>();
        _grabbedObjRb.velocity = Vector3.zero;
        _grabbedObjRb.freezeRotation= true;
        _grabbedObjRb.useGravity = false;
        _isThereObj = true;
    }
    private void ReleaseObject()
    {
        _isThereObj = false;
        _grabbedObjRb.velocity = Vector3.zero;
        _grabbedObjRb.freezeRotation = false;
        _grabbedObjRb.useGravity = true;
    }
}
