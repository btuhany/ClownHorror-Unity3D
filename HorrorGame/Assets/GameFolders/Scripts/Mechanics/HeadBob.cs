using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] bool _enable;
    [SerializeField][Range(0f, 0.1f)] float _amplitude = 0.15f;
    [SerializeField][Range(0f, 30f)] float _freq = 10f;
    [SerializeField] Transform _camera;
    [SerializeField] Transform _cameraHolder;

    float _toggleSpeed = 3f;
    Vector3 _startPos;
    CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _startPos= _camera.localPosition;
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _freq) * _amplitude;
        pos.x += Mathf.Cos(Time.time * _freq / 2) * _amplitude * 2;
        return pos;
    }
    private void CheckMotion()
    {
        
    }
}
