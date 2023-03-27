using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitEffectHandler : MonoBehaviour
{
    private Vector3 _targetPos;
    private Vector3 _currentPos;
    private Vector3 _defaultPos = new Vector3(0, 0.1436931f, 0.001278264f);
    
    [SerializeField] private float _headStrechScale = 2.5f;
    [SerializeField] private float _snapiness = 10f;
    [SerializeField] private float _returnSpeed =5f;
    private void Update()
    {
        if (_targetPos != _defaultPos)
            _targetPos = Vector3.Lerp(_targetPos, _defaultPos, _returnSpeed * Time.deltaTime);
        if (_currentPos != _targetPos)
        {
            _currentPos = Vector3.Slerp(_currentPos, _targetPos, _snapiness * Time.fixedDeltaTime);
            transform.localPosition = _currentPos;
        }
    }
    public void HitImpact(RaycastHit hit)
    {
        Debug.Log(this.gameObject.name);
        Vector3 normal = hit.normal;
        _targetPos += new Vector3(-normal.x,-normal.y,normal.z)/ _headStrechScale;
    }

}
