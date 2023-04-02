using Abstracts;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DoorController : Interactable
{
    [SerializeField] AudioClip _unlockAudio;
    [SerializeField] AudioClip _openAudio;
    [SerializeField] AudioClip _closeAudio;
    [SerializeField] private CollectableID _requirementItem;

    AudioSource _audio;
    bool _isOpened;
    bool _isPlaying;
    bool _isUnlocked;
    private void Awake()
    {
        _audio= GetComponent<AudioSource>();
    }
    public override void Interact()
    {
        if(PlayerInventoryManager.Instance.IsInInventory(_requirementItem))
        {
            if(!_isUnlocked)
            {
                _audio.PlayOneShot(_unlockAudio);
                _isUnlocked= true;

            }
            else
            {
                OpenOrClose();
            }

        }
    }
    private void OpenOrClose()
    {
        if(_isPlaying) { return; }
        if(_isOpened)
        {
            _audio.PlayOneShot(_closeAudio);
            transform.DOLocalRotate(new Vector3(0, 0, 0), 0.3f);
            StartCoroutine(SetIsOpened(false, 0.3f));
           
        }
        else
        {
            
            _audio.PlayOneShot(_openAudio);
            transform.DOLocalRotate(new Vector3(0, 90, 0), 2.5f);
            StartCoroutine(SetIsOpened(true,2.5f));
        }
    }
    IEnumerator SetIsOpened(bool state, float duration)
    {
        _isPlaying = true;
        yield return new WaitForSeconds(duration);
        _isOpened = state;
        _isPlaying = false;
        yield return null;

    }
}
