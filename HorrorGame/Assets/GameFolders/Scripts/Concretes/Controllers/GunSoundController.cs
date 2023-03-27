using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSoundController : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip _handGunSound;

    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void ShootingSound()
    {
        _audioSource.PlayOneShot(_handGunSound);
    }
}
