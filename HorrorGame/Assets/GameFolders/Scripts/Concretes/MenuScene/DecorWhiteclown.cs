using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DecorWhiteclown : MonoBehaviour
{
    Vector3 position1;
    [SerializeField] Transform position2;
    private bool _isStop;

    private void Awake()
    {
        position1 = transform.position;
    }
    private void Update()
    {
        if (_isStop)
        {
            return;
        }
        
        if(transform.position.y == position2.position.y)
        {
            transform.DOMove(position1, 4f);
        }
        else if(transform.position.y == position1.y)
        {
            transform.DOMove(position2.position, 4f);
        }
        
    }
    public void DoStop()
    {
        _isStop = true;
        transform.DOKill();
    }
}
