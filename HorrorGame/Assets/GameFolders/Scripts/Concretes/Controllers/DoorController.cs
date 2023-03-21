using Abstracts;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : Interactable
{
    bool _isOpened;
    public override void Interact()
    {
        OpenOrClose();
    }
    private void OpenOrClose()
    {
        if(_isOpened)
        {
            transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
            _isOpened= false;
        }
        else
        {
            transform.DOLocalRotate(new Vector3(0, 90, 0), 1);
            _isOpened = true;
        }
    }
}
