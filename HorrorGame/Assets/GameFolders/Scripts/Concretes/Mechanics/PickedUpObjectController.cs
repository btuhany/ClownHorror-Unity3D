using Abstracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUpObjectController : MonoBehaviour
{
    [SerializeField] float _objectGrabbedMovingSpeed;
    [SerializeField] Transform _grabPoint;


    PickUpAble _pickedUpObj;  
    Rigidbody _pickedUpObjectRb;
    

    public bool IsThereObj => _pickedUpObj;

    public void ThrowObject(float force, Vector3 dir)
    {
        if (!_pickedUpObj) return;  
        _pickedUpObj.Throwed((_grabPoint.forward + dir / 3), force); //groundMovement vector affects throwing
        _pickedUpObj = null;
        _pickedUpObjectRb = null;
    }
    public bool PickUpOrDrop(PickUpAble pickUpAbleObj)  //return true if pickedup (for raycester to stop raycasting)
    {
        if(_pickedUpObj)
        {
            ReleaseObject();
            return false;
        }
        else
        {
            GrabObject(pickUpAbleObj);
            return true;
        }
    }
    private void GrabObject(PickUpAble pickUpAbleObj)
    {
        pickUpAbleObj.Grabbed();
        _pickedUpObj = pickUpAbleObj;
        _pickedUpObjectRb = pickUpAbleObj.Rb;

    }
    private void ReleaseObject()
    {
        _pickedUpObj.Released();
        _pickedUpObj = null;
        _pickedUpObjectRb = null;
    }
    private void LateUpdate()
    {
        if (_pickedUpObj)
        {
            Vector3 pos = _grabPoint.position - _pickedUpObj.transform.position;
            Vector3 newVelocity = pos * _objectGrabbedMovingSpeed * pos.magnitude;

            _pickedUpObjectRb.MoveRotation(transform.rotation);
            _pickedUpObjectRb.velocity = newVelocity;
        }
    }
    //private void Update()
    //{
    //    Debug.DrawRay(_playerCamera.position, _playerCamera.forward * _pickUpDistance, Color.red);
    //}
}
