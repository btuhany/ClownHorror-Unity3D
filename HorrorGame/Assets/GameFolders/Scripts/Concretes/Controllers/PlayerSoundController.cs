using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// var sounds should be in awake chaching..
public class PlayerSoundController : MonoBehaviour
{
    [Header("Sound Waves")]
    [SerializeField] LayerMask _layer;
    [SerializeField] float _walkingSoundLevel;
    [SerializeField] float _runningSoundLevel;
    

    [Header("Sound Playing Frequencies In A Loop (shorter is faster)")]
    [SerializeField][Range(0.5f,2)] float _walkTime;
    [SerializeField][Range(0.1f,1)] float _runTime;


    [Header("Audio Clips")]
    [SerializeField] private List<AudioClip> _walkFootSteps = new List<AudioClip>();
    [SerializeField] private List<AudioClip> _runFootSteps = new List<AudioClip>();
    [SerializeField] private AudioClip _toggleLight;

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
        CreateSoundWaves(_walkingSoundLevel, SoundType.Moderate, _layer);
        _walkTimeCounter = _walkTime;
    }
    public void PlayRunFootStep()
    {
        if (_runTimeCounter > 0) return;
            _audioSource.PlayOneShot(_runFootSteps[Random.Range(0, _runFootSteps.Count)]);
        CreateSoundWaves(_runningSoundLevel, SoundType.Serious, _layer);
        _runTimeCounter = _runTime;
    }

    public void PlayToggleLight()
    {
        _audioSource.PlayOneShot(_toggleLight);
    }
    private void CreateSoundWaves(float range, SoundType soundType, LayerMask layer)
    {
        var sound = new Sound(transform.position, range, soundType, layer);
        Sounds.CreateWaves(sound);
    }


}
