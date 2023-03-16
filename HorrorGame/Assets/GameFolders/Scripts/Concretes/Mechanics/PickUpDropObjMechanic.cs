using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDropObjMechanic : MonoBehaviour
{
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
            _grabbedObj = hit.collider.gameObject;
            GrabObject();
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
        _grabbedObjRb.AddForce((_grabPoint.forward + dir/3)* force);
    }

    //private void Update()
    //{
    //     // Debug.DrawRay(_playerCamera.position, _playerCamera.forward * _pickUpDistance, Color.red);
    //}

    private void LateUpdate()
    {
        if(_isThereObj)
        {
            //_grabbedObjRb.MovePosition(_grabPoint.position);
            _grabbedObj.transform.position = _grabPoint.transform.position;  // other method (rigidbody) cause jittering
            _grabbedObj.transform.rotation = _grabPoint.transform.rotation;
            //_grabbedObj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }
    }
    private void GrabObject()
    {       
        _grabbedObjRb = _grabbedObj.GetComponent<Rigidbody>();
        _grabbedObjRb.isKinematic = true;
        _isThereObj = true;
    }
    private void ReleaseObject()
    {
        _isThereObj = false;
        _grabbedObjRb.isKinematic = false;
        //_grabbedObj.transform.localScale = new Vector3(0.6f, 0.8f, 0.6f);
    }
}
