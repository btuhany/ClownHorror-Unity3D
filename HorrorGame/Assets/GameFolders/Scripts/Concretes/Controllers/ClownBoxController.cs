using Abstracts;
using AI;
using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownBoxController : PickUpAble
{
    [SerializeField] PickedUpObjectController _pickedUpController;
    [SerializeField] List<AudioClip> _audioClips = new List<AudioClip>();
    [SerializeField] float _maxBurnTime = 5f;
    [SerializeField] AiEnemy _ai;
    [SerializeField] SliderController _slider;
    AudioSource _audio;
    float _burnTimer;



    private void OnEnable()
    {
        _audio = GetComponent<AudioSource>();
        _burnTimer = _maxBurnTime;
        _audio.clip = _audioClips[0];
        _audio.Play();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire") && IsGrabbed)
        {
            _slider.gameObject.SetActive(true);
            _ai.IsClownBoxBurning();
            SoundManager.Instance.PlaySoundFromSingleSource(0);
            _audio.Stop();
            _audio.clip = _audioClips[1];
            _audio.Play();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Fire") && IsGrabbed)
        {   
            _burnTimer -= Time.deltaTime;
            _slider.SetSlider(_burnTimer);
            if (_burnTimer < 0)
            {
                _slider.gameObject.SetActive(false);
                _pickedUpController.ReleaseObject();
                GameManager.Instance.ClownEvent();
                Destroy(other.gameObject, 0.2f);
                Destroy(this.gameObject, 0.5f);
                    
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _slider.gameObject.SetActive(false);
        _burnTimer = _maxBurnTime;
        _audio.Stop();
        _audio.clip = _audioClips[0];
        _audio.Play();
    }

}
