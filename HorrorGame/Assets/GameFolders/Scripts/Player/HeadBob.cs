using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] bool _enableStabilizing = true;
    [SerializeField][Range(0f, 0.1f)] float _amplitudeWalking = 0.15f;
    [SerializeField][Range(0f, 30f)] float _freqWalking = 10f;
    [SerializeField][Range(0f, 0.1f)] float _amplitudeRunning = 0.15f;
    [SerializeField][Range(0f, 30f)] float _freqRunning = 10f;
    [SerializeField] Transform _camera;
    [SerializeField] Transform _cameraHolder;

    float _timeCounter;
    Vector3 _startPos;
    CharacterControllerMovement _character;

    private void Awake()
    {
        _startPos= _camera.localPosition;
        _character= GetComponent<CharacterControllerMovement>();
    }

    public void WalkingHeadBob()
    {
        if (_enableStabilizing)
            _camera.LookAt(FocusTarget());
        _camera.localPosition += FootStepMotion(_amplitudeWalking, _freqWalking);

    }
    public void RunningHeadBob()
    {
        if (_enableStabilizing)
            _camera.LookAt(FocusTarget());
        _camera.localPosition += FootStepMotion(_amplitudeRunning, _freqRunning);
    }
    public void ResetPosition()
    {
        if (_camera.localPosition == _startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, Time.deltaTime);
        _timeCounter = 0;
    }
    private Vector3 FocusTarget()
    {
        Vector3 pos = _cameraHolder.position;
        pos += _cameraHolder.forward * 15f;
        return pos;
    }
   
    private Vector3 FootStepMotion(float amplitude, float freq)
    {
        _timeCounter += Time.deltaTime;
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(_timeCounter * freq) * amplitude;
        pos.x += Mathf.Cos(_timeCounter * freq / 2) * amplitude * (-1.5f);
        return pos;
    }


}
