using Enums;
using System.Collections.Generic;
using UnityEngine;
using Mechanics;
using Unity.VisualScripting;

// var sounds should be in awake chaching.. Create walk and run sound waves in awaking?
namespace Controllers
{
    public class PlayerSoundController : MonoBehaviour, ICreateSound
    {
        [Header("Sound Waves")]
        [SerializeField] LayerMask _layer;
        [SerializeField] float _walkingSoundLevel;
        [SerializeField] float _runningSoundLevel;


        [Header("Sound Playing Frequencies In A Loop (shorter is faster)")]
        [SerializeField][Range(0.5f, 2)] float _walkTime;
        [SerializeField][Range(0.1f, 1)] float _runTime;


        [Header("Audio Clips")]
        [SerializeField] private List<AudioClip> _walkFootSteps = new List<AudioClip>();
        [SerializeField] private List<AudioClip> _runFootSteps = new List<AudioClip>();
        [SerializeField] private AudioClip _toggleLight;

        private AudioSource _audioSource;
        private float _walkTimeCounter = 0;
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
            CreateSoundWaves(_walkingSoundLevel, SoundType.Moderate, _layer, this.gameObject);
            _walkTimeCounter = _walkTime;
        }
        public void PlayRunFootStep()
        {
            if (_runTimeCounter > 0) return;
            _audioSource.PlayOneShot(_runFootSteps[Random.Range(0, _runFootSteps.Count)]);
            CreateSoundWaves(_runningSoundLevel, SoundType.Serious, _layer, this.gameObject);
            _runTimeCounter = _runTime;
        }

        public void PlayToggleLight()
        {
            _audioSource.PlayOneShot(_toggleLight);
        }
        public void CreateSoundWaves(float range, SoundType soundType, LayerMask layer, GameObject gameObj)
        {
            var sound = new Sound(transform.position, range, soundType, layer, gameObj);
            Sounds.CreateWaves(sound);
        }


    }

}
