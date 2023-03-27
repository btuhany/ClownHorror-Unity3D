using Abstracts;

using UnityEngine;

namespace Controllers
{
    public class FragileObjectController : PickUpAble
    {
        [SerializeField] ParticleSystem _breakedFX;
        private void OnCollisionEnter(Collision collision)
        {
            if (IsThrowed)
            {
                CreateTheSoundWave();
                _breakedFX.Play();
                Destroy(this.gameObject,0.5f);
            }
                
        }
    }
}

