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
        [SerializeField] private AudioClip _breathAimed;
        [SerializeField] private AudioClip _breathCrouch;
        [SerializeField] private AudioClip _breathJump;
        [SerializeField] private AudioClip _noAmmoSound;
        [SerializeField] private AudioClip[] _takeHitSound;
        [SerializeField] private AudioClip _woundedSound;
        private AudioSource _audioSource;
        private float _walkTimeCounter = 0;
        private float _runTimeCounter = 0;
        private float _aimedBreathCounter = 0;


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
            if(_aimedBreathCounter != 0)
            {
                _aimedBreathCounter -= Time.deltaTime;
                _aimedBreathCounter = Mathf.Max(0, _aimedBreathCounter);
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
        public void PlayNoAmmo()
        {
            _audioSource.PlayOneShot(_noAmmoSound);
        }
        public void PlayBreathAimed()
        {
            if (_aimedBreathCounter > 0) return;
            _audioSource.PlayOneShot(_breathAimed,0.6f);
            _aimedBreathCounter = 7.4f; //length of the clip
        }
        public void StopBreathSound()
        {
            if (_aimedBreathCounter == 0) return;
            _aimedBreathCounter = 0f;
            _audioSource.Stop();
        }
        public void PlayToggleLight()
        {
            _audioSource.PlayOneShot(_toggleLight);
        }
        public void PlayCrouch()
        {
            _audioSource.PlayOneShot(_breathCrouch,0.4f);
        }
        public void PlayJump()
        {
            _audioSource.PlayOneShot(_breathJump,0.5f);
        }
        public void PlayTakeHit()
        {
            _audioSource.PlayOneShot(_takeHitSound[Random.Range(0,_takeHitSound.Length)]);
        }

        public void CreateSoundWaves(float range, SoundType soundType, LayerMask layer, GameObject gameObj)
        {
            var sound = new Sound(transform.position, range, soundType, layer, gameObj);
            Sounds.CreateWaves(sound);
        }


    }

}
