using Abstracts;
using Unity;
using UnityEngine;
using DG.Tweening;
namespace Controllers
{
    public class FragileObjectController : PickUpAble
    {
        
        [SerializeField] ParticleSystem _breakedFX;
        [SerializeField] [Range(0.1f,0.7f)] float _destroyTime=0.3f;
        private void OnCollisionEnter(Collision collision)
        {
            if (IsThrowed)
            {
                CreateTheSoundWave();
                _audioSource.PlayOneShot(_throwedAudioClips[Random.Range(0,_throwedAudioClips.Count)]);
                _breakedFX.transform.SetParent(null);
                _breakedFX.Play();
                transform.DOScale(Vector3.zero, _destroyTime);
                Destroy(this.gameObject, _destroyTime+0.35f);
            }
                
        }
    }
}

