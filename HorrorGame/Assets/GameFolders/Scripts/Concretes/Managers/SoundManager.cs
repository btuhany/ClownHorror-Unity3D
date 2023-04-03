using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoObject<SoundManager>
{
    [SerializeField] AudioSource[] _audioSources;
    private void Awake()
    {
        SingletonThisObject(this);
        _audioSources = GetComponentsInChildren<AudioSource>();
    }
    public void PlaySound(int index)
    {
        _audioSources[index].Play();
    }


}
