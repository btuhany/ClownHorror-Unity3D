using Abstracts;
using Controllers;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonFragileObjectController : PickUpAble
{

    private void OnCollisionEnter(Collision collision)
    {
        if (IsThrowed)
        {
            StopAllCoroutines();
            StartCoroutine(ResetIsThrowed());
            CreateTheSoundWave();
            _audioSource.PlayOneShot(_throwedAudioClips[Random.Range(0, _throwedAudioClips.Count)]);
        }
    }
    IEnumerator ResetIsThrowed()
    {
        yield return new WaitForSeconds(2f);
        IsThrowed = false;
        yield return null;
    }
}
