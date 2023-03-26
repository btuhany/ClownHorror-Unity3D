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

    private Vector3 _curentRotation;
    private Vector3 _tragetRotation;
    private void Update()
    {

        if (_tragetRotation != Vector3.zero)
            _tragetRotation = Vector3.Lerp(_tragetRotation, Vector3.zero, _returnSpeed * Time.deltaTime);
        if (_curentRotation != _tragetRotation)
        {
            _curentRotation = Vector3.Slerp(_curentRotation, _tragetRotation, _snapiness * Time.fixedDeltaTime);
            transform.localRotation = Quaternion.Euler(_curentRotation);
        }



    }
    public void RecoilEffect()
    {
        int randomSign = Random.Range(0, 2);
        if (randomSign == 0)
            randomSign = -1;
        _tragetRotation += new Vector3(Random.Range(_minRecoilX, _maxRecoilX), Random.Range(_minRecoilY, _maxRecoilY) * randomSign, Random.Range(_minRecoilZ, _maxRecoilZ));
    }
}
