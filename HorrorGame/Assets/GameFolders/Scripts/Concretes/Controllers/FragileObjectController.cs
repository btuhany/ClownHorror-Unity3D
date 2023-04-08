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
        bool _isBroken;
        private void OnCollisionEnter(Collision collision)
        {
            if (IsThrowed && !_isBroken)
            {
                _isBroken = true;
                CreateTheSoundWave();
                _audioSource.PlayOneShot(_throwedAudioClips[Random.Range(0,_throwedAudioClips.Count)]);
                _audioSource.DOFade(0,_destroyTime + 0.2f);
                _breakedFX.transform.SetParent(null);
                _breakedFX.Play();
                transform.DOScale(Vector3.zero, _destroyTime-0.1f);
                Destroy(this.gameObject, _destroyTime+0.35f);
            }
                
        }
    }
}

