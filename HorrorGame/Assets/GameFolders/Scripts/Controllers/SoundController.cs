using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("Sound Playing Frequencies In A Loop (shorter is faster)")]
    [SerializeField][Range(0.5f,2)] float _walkTime;
    [SerializeField][Range(0.1f,1)] float _runTime;


    [Header("Audio Clips")]
    [SerializeField] private List<AudioClip> _walkFootSteps = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _runFootSteps = new List<AudioClip>();

    private AudioSource _audioSource;
    private float _walkTimeCounter=0;
    private float _runTimeCounter = 0;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_walkTimeCounter != 0)
        {
            _walkTimeCounter -= Time.deltaTime;
            _walkTimeCounter = Mathf.Max(0, _walkTimeCounter);
        }
        if (_runTimeCounter != 0)
        {
            _runTimeCounter -= Time.deltaTime;
            _runTimeCounter = Mathf.Max(0, _runTimeCounter);
        }

    }

    public void PlayWalkFootStep()
    {
        if (_walkTimeCounter > 0) return;
        _audioSource.PlayOneShot(_walkFootSteps[Random.Range(0, _walkFootSteps.Count)]);
        _walkTimeCounter = _walkTime;
    }
    public void PlayRunFootStep()
    {
        if (_runTimeCounter > 0) return;
        _audioSource.PlayOneShot(_runFootSteps[Random.Range(0, _runFootSteps.Count)]);
        _runTimeCounter = _runTime;
    }


}
