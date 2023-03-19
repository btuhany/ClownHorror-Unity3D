using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] Transform _groundCheckTransform;
    [SerializeField] float _groundDistance;
    [SerializeField] LayerMask _groundMask;


    
    bool _isGrounded;
    bool _isGroundedLastFrame;
    public bool IsGrounded { get => _isGrounded; }
    public Vector3 GroundCheckLocalPos => _groundCheckTransform.localPosition;
    
    public event System.Action OnLanded;
    private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheckTransform.position, _groundDistance, _groundMask);
        if (_isGrounded && !_isGroundedLastFrame)  //Check landing
            OnLanded?.Invoke();
        _isGroundedLastFrame = _isGrounded;
       // Debug.Log(_groundCheckTransform.position);
    }
    //public void NewGroundCheckPos(Vector3 pos)
    //{
    //    _groundCheckTransform.localPosition = pos;
    //}
    //public void ResetGroundCheckPos()
    //{
    //    _groundCheckTransform.localPosition = _localPosAtStart;
    //}

}
