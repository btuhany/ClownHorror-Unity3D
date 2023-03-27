using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CamRecoilEffectHandler : MonoBehaviour
{

    [SerializeField] private float _minRecoilX = -2;
    [SerializeField] private float _maxRecoilX = -5;
    [SerializeField] private float _minRecoilY = -3;
    [SerializeField] private float _maxRecoilY =4 ;
    [SerializeField] private float _minRecoilZ = 0.1f;
    [SerializeField] private float _maxRecoilZ = 0.34f;

    [SerializeField] private float _snapiness = 6;
    [SerializeField] private float _returnSpeed = 2;

    private Vector3 _currentRotation;
    private Vector3 _targetRotation;
    private void Update()
    {

        if (_targetRotation != Vector3.zero)
            _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
        if (_currentRotation != _targetRotation)
        {
            _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _snapiness * Time.fixedDeltaTime);
            transform.localRotation = Quaternion.Euler(_currentRotation);
        }



    }
    public void RecoilEffect()
    {
        int randomSign = Random.Range(0, 2);
        if (randomSign == 0)
            randomSign = -1;
        _targetRotation += new Vector3(Random.Range(_minRecoilX, _maxRecoilX), Random.Range(_minRecoilY, _maxRecoilY) * randomSign, Random.Range(_minRecoilZ, _maxRecoilZ));
    }
}
