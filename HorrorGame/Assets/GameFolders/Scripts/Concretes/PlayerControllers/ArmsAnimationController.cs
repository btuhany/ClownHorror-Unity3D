using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsAnimationController : MonoBehaviour
{
    Animator _anim;



    private bool _isJumped;  //for in air shooting; !!!!!!!!!


    private bool _isAimed;
    private bool _isRunning;

    private int _isAimedHash;
    private int _isShootedHash;
    private int _isRunningHash;
    private int _isJumpedHash;
    private int _isInAirShoot;
    private int _isNoAmmoHash;
    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _isAimedHash = Animator.StringToHash("IsAimed");
        _isShootedHash = Animator.StringToHash("IsShooted");
        _isRunningHash = Animator.StringToHash("IsRunning");
        _isJumpedHash = Animator.StringToHash("IsJumped");
        _isInAirShoot = Animator.StringToHash("IsInAirShoot");
        _isNoAmmoHash = Animator.StringToHash("IsNoAmmo");
    }
    public void Aimed(bool isAimed)
    {
        if(_isAimed == isAimed)
        {
            return;
        }
        _isAimed = isAimed;
        
        _anim.SetBool(_isAimedHash, isAimed);
    }
    public void NoAmmo()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("IsNoAmmo")) return;
        _anim.ResetTrigger(_isNoAmmoHash);
        _anim.SetTrigger(_isNoAmmoHash);
         
    }
    public void Shooted()
    {
        
        if(_isJumped)
        {
            _anim.SetBool(_isInAirShoot, true);
            return;
        }
        
        _anim.SetTrigger(_isShootedHash);
    }
    public void Running(bool isRunning)
    {
        if(_isRunning == isRunning)
        {
            return;
        }
        _isRunning = isRunning;
        _anim.SetBool(_isRunningHash, isRunning);
    }
    public void Jumped()
    {
        if (_isAimed)
        {
            _anim.ResetTrigger(_isJumpedHash);
        }
        else
        {
            _isJumped = true;
            StartCoroutine(IsJumpedReset());
            _anim.SetTrigger(_isJumpedHash);
        }     
    }
    private IEnumerator IsJumpedReset()
    {
        yield return new WaitForSeconds(1f);
        _isJumped = false;
        _anim.SetBool(_isInAirShoot, false);
        yield return null;
    }
}
