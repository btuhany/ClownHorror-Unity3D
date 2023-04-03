using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoObject<SoundManager>
{
    [SerializeField] AudioClip[] _enemyActionAudioClips;
    AudioSource[] _audioSources;
    private void Awake()
    {
        SingletonThisObject(this);
        _audioSources = GetComponentsInChildren<AudioSource>();
    }
    public void PlaySoundFromSingleSource(int index)
    {
        _audioSources[index].Play();
    }
    public void EnemyActionSounds(int index)
    {
        _audioSources[2].PlayOneShot(_enemyActionAudioClips[index]);
    }


}
