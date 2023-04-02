using Abstracts;
using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownBoxController : PickUpAble
{
    [SerializeField] PickedUpObjectController _pickedUpController;
    [SerializeField] float _maxBurnTime = 5f;

    float _burnTimer;

    bool _isBurning = false;
    private void OnEnable()
    {
        _burnTimer = _maxBurnTime;


    }
    private void Update()
    {
        if(_burnTimer<_maxBurnTime && !_isBurning)
        {
            _burnTimer += Time.deltaTime;
        }
        if (IsGrabbed)
        {
            //ses
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(IsThrowed)
        {
            StartCoroutine(ResetIsThrowed());
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Fire"))
        {
            if(IsGrabbed)
            {
                _isBurning = true;
                _burnTimer -= Time.deltaTime;
                if(_burnTimer < 0)
                {
                    _pickedUpController.ReleaseObject();
                    GameManager.Instance.ClownEvent();
                    Destroy(other.gameObject, 0.2f);
                    Destroy(this.gameObject, 0.5f);
                    
                }
            }
        }
    }

    IEnumerator ResetIsThrowed()
    {
        yield return new WaitForSeconds(2f);
        IsThrowed = false;
        yield return null;
    }
}
