using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSoundController : MonoBehaviour
{
    [SerializeField] AudioClip[] _audioClips;

    AudioSource _audioSource;

    private void Awake()
    {
        _audioSource= GetComponent<AudioSource>();
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
