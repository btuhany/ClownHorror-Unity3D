
using Controllers;
using UnityEngine;

namespace Abstracts
{
    public abstract class MovableThrowable : Interactable
    {

        [SerializeField] float _soundRange;
        [SerializeField] LayerMask _soundWaveLayer; //who/which layer can hear
        private bool _isThrowed;

        Rigidbody _rb;
        public Rigidbody Rb { get => _rb; }
        public override void Interact()
        {
            Grabbed();
        }

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
        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (_isThrowed)
            {
                CreateSoundWaves(_soundRange, SoundType.Serious, _soundWaveLayer);  //_layer=7 enemy
                _isThrowed = false;
            }
        }

        private void CreateSoundWaves(float range, SoundType soundType, LayerMask layer)
        {
            var sound = new Sound(transform.position, range, soundType, layer);
            Sounds.CreateWaves(sound);
        }

    }
}

