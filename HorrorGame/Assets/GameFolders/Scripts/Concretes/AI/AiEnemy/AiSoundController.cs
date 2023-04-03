using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiSoundController : MonoBehaviour
{
    [SerializeField] AudioClip[] _audioClips;
    [SerializeField] AudioClip[] _runFootStepsClips;
    [SerializeField] AudioClip[] _walkFootStepsClips;
    [SerializeField] AudioClip[] _rotateClips;
    [SerializeField] float[] _maxRunFootStepTime;
    [SerializeField] float[] _maxWalkFootStepTime;

    AudioSource _audioSource;
    NavMeshAgent _agent;
    float _currentSpeed;
    float _footStepTimer;
    float _currentMaxFootStepTime;
 
    private void Awake()
    {
        
        _agent = GetComponent<NavMeshAgent>();
        _audioSource= GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (_agent.velocity.magnitude <= 0.2) return;
        else if(_agent.velocity.magnitude <= 6f)
        {
            if (!(_currentSpeed >= _agent.velocity.magnitude * 0.89f && _currentSpeed <= _agent.velocity.magnitude * 1.1f))
            {
                ChangeWalkFootStepTimer();
            }
            _footStepTimer -= Time.deltaTime;
            if (_footStepTimer < 0)
            {
                PlayWalkingFootStep();
                _footStepTimer = _currentMaxFootStepTime;
            }
        }
        else
        {
            if (!(_currentSpeed >= _agent.velocity.magnitude * 0.89f && _currentSpeed <= _agent.velocity.magnitude * 1.1f))
            {
                ChangeRunFootStepTimer();
            }

            _footStepTimer -= Time.deltaTime;
            if (_footStepTimer < 0)
            {
                PlayRunningFootStep();
                _footStepTimer = _currentMaxFootStepTime;
            }
        }

    }

    private void ChangeWalkFootStepTimer()
    {
        float speed = _agent.velocity.magnitude;
        if (speed >= 6f)
        {
            _currentMaxFootStepTime = _maxWalkFootStepTime[5];
        }
        else if (speed >= 5.5f)
        {
            _currentMaxFootStepTime = _maxWalkFootStepTime[4];
        }
        else if (speed >= 5f)
        {
            _currentMaxFootStepTime = _maxWalkFootStepTime[3];
        }
        else if (speed >= 4.4)
        {
            _currentMaxFootStepTime = _maxWalkFootStepTime[2];
        }
        else if (speed >= 4f)
        {
            _currentMaxFootStepTime = _maxWalkFootStepTime[1];
        }
        else if (speed >= 3)
        {
            _currentMaxFootStepTime = _maxWalkFootStepTime[0];
        }

    }

    private void PlayWalkingFootStep()
    {
        _audioSource.PlayOneShot(_walkFootStepsClips[Random.Range(0, _walkFootStepsClips.Length)], 0.7f);
    }

    private void ChangeRunFootStepTimer()  //or raycast check?
    {
        float speed = _agent.velocity.magnitude;
        if(speed >= 23f)
        {
            _currentMaxFootStepTime = _maxRunFootStepTime[5]; //0.15
        }
        else if(speed >= 19f)
        {
            _currentMaxFootStepTime =_maxRunFootStepTime[4];//0.25
        }
        else if(speed >= 14)
        {
            _currentMaxFootStepTime = _maxRunFootStepTime[3]; //0.28
        }
        else if(speed >= 8.2f)
        {
            _currentMaxFootStepTime = _maxRunFootStepTime[2];// 0.32
        }
        else if (speed >= 6.9f)
        {
            _currentMaxFootStepTime = _maxRunFootStepTime[1];  //0.34
        }
        else if (speed >= 6)
        {
            _currentMaxFootStepTime = _maxRunFootStepTime[0]; //0.5
        }
        else if(speed >= 0.1f)
        {
            _currentMaxFootStepTime = 0.5f;
        }

        

    }
    
    private void PlayRunningFootStep()
    {
        _audioSource.PlayOneShot(_runFootStepsClips[Random.Range(0, _runFootStepsClips.Length)],0.7f);
    }
    public void Rotate()
    {
        _audioSource.PlayOneShot(_rotateClips[Random.Range(0,_rotateClips.Length)],0.8f);
    }
    public void StunAgonize()
    {
        _audioSource.PlayOneShot(_audioClips[4]);
    }
    public void AgonizeEnd()
    {
        _audioSource.PlayOneShot(_audioClips[5]);
    }
    public void AttackLaugh()
    {
        _audioSource.PlayOneShot(_audioClips[2]);
        _audioSource.PlayOneShot(_audioClips[3],0.6f);
    }
    public void PlayerFound()
    {
        _audioSource.PlayOneShot(_audioClips[0]);
    }
    public void ChaseOver()
    {
        _audioSource.PlayOneShot(_audioClips[1]);
    }
}
