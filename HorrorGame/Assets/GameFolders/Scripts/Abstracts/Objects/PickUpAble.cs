using Enums;

using UnityEngine;
using Mechanics;

namespace Abstracts
{
    public abstract class PickUpAble : Targetable
    {

        [SerializeField] float _soundRange;
        [SerializeField] LayerMask _soundWaveLayer; //who/which layer can hear
        protected bool _isThrowed;

        Rigidbody _rb;
        public Rigidbody Rb { get => _rb; }
        

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Grabbed()
        {
            _rb.velocity = Vector3.zero;
            _rb.freezeRotation = true;
            _rb.useGravity = false;
        }
        public void Released()
        {
            _rb.velocity = Vector3.zero;
            _rb.freezeRotation = false;
            _rb.useGravity = true;
        }
        public void Throwed(Vector3 dir, float force)
        {
            _isThrowed = true;
            Released();
            _rb.AddForce(dir * force);
        }
        protected void CreateTheSoundWave()
        {
            CreateSoundWaves(_soundRange, SoundType.Serious, _soundWaveLayer);  //_layer=7 enemy
        }

        private void CreateSoundWaves(float range, SoundType soundType, LayerMask layer)
        {
            var sound = new Sound(transform.position, range, soundType, layer);
            Sounds.CreateWaves(sound);
        }

    }
}

