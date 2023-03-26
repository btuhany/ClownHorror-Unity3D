using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsAnimationController : MonoBehaviour
{
    Animator _anim;

    private bool _isAimed;
    private bool _isRunning;



    private int _isAimedHash;
    private int _isShootedHash;
    private int _isRunningHash;
    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _isAimedHash = Animator.StringToHash("IsAimed");
        _isShootedHash = Animator.StringToHash("IsShooted");
        _isRunningHash = Animator.StringToHash("IsRunning");
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
    public void Shooted()
    {
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
}
