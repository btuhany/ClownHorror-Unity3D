using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoObject<SoundManager>
{
    [SerializeField] AudioClip[] _enemyActionAudioClips;
    AudioSource[] _audioSources;


    float _heartbeatCounter=3f;
    private void Awake()
    {
        SingletonThisObject(this);
        _audioSources = GetComponentsInChildren<AudioSource>();
    }

    public void PlaySoundFromSingleSource(int index)
    {
        _audioSources[index].Play();
    }
    public void StopSoundSource(int index)
    {
        _audioSources[index].Stop();
    }
    public void EnemyActionSounds(int index)
    {
        _audioSources[2].PlayOneShot(_enemyActionAudioClips[index]);
    }
    public void StartHeartbeatLoop()
    {
        _audioSources[4].Play();
    }
    public void SetHeartbeatSpeed(float value,float delay)
    {
        if (_audioSources[4].isPlaying)
        {
            value = Mathf.Clamp(value,0.8f, 2.5f);
            _heartbeatCounter -= Time.deltaTime;
            if (_heartbeatCounter < 0)
            {
                _heartbeatCounter = delay;
                _audioSources[4].DOPitch(value, delay);
            }
        }
    }
    public void ResetStopHeartbeat()
    {
        _heartbeatCounter = 3f;
        _audioSources[4].pitch = 1f;
        _audioSources[4].Stop();
    }

}
