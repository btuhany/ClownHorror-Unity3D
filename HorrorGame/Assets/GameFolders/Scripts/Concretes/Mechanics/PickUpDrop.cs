using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDrop : MonoBehaviour
{
    [SerializeField] Transform _playerCamera;
    [SerializeField] Transform _grabPoint;
    [SerializeField] LayerMask _layer;
    [SerializeField] float _pickUpDistance;
    GameObject _gameObj;
    Rigidbody _rbObj;
    bool _isHolding;
    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.E) && !_isHolding)
        {
            if(Physics.Raycast(_playerCamera.position, _playerCamera.forward, out RaycastHit hit, _pickUpDistance, _layer))
            {
            
                _gameObj = hit.collider.gameObject;
                _rbObj = _gameObj.GetComponent<Rigidbody>();
                _rbObj.useGravity= false;

                _isHolding = true;
            }
            
        }
        else if(Input.GetKeyDown(KeyCode.E) && _isHolding)
        {
            _isHolding = false;
            _rbObj.useGravity = true;
            _rbObj = null;

        }

        if(_isHolding)
        {
            _rbObj.MovePosition(_grabPoint.position);
            _rbObj.MoveRotation(_grabPoint.rotation);
        }
       // Debug.DrawRay(_playerCamera.position, _playerCamera.forward * _pickUpDistance, Color.red);
    }
}
