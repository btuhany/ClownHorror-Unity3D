using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class RecoilEffectHandler : MonoBehaviour
{


    [SerializeField] private float _minRecoilX;
    [SerializeField] private float _maxRecoilX;
    [SerializeField] private float _minRecoilY;
    [SerializeField] private float _maxRecoilY;
    [SerializeField] private float _minRecoilZ;
    [SerializeField] private float _maxRecoilZ;

    [SerializeField] private float _snapiness;
    [SerializeField] private float _returnSpeed;

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
        _tragetRotation += new Vector3(Random.Range(_minRecoilX, _maxRecoilX), Random.Range(_minRecoilY, _maxRecoilY), Random.Range(_minRecoilZ, _maxRecoilZ));
    }
}
