using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsAnimationController : MonoBehaviour
{
    Animator _anim;

    private bool _isAiming;



    private int _isAimed;
    private int _isShooted;
    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _isAimed = Animator.StringToHash("IsAimed");
        _isShooted = Animator.StringToHash("IsShooted");
    }
    public void Aimed(bool isAimed)
    {
        if(_isAiming == isAimed)
        {
            return;
        }
        _isAiming = isAimed;
        Debug.Log(_isAiming);
        _anim.SetBool(_isAimed, isAimed);
    }
    public void Shooted()
    {
        _anim.SetTrigger(_isShooted);
    }
}
