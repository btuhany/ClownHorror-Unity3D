using Abstracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LastBarrelController : Interactable
{
    [SerializeField] GameObject _fire;
    [SerializeField] List<AudioClip> _audioClips= new List<AudioClip>();
    AudioSource _audio;
    bool _isLightenedUp;
    private void Awake()
    {
        _audio= GetComponent<AudioSource>();
    }
    public override void Interact()
    {
        if(PlayerInventoryManager.Instance.IsInInventory(CollectableID.Fuel) && PlayerInventoryManager.Instance.IsInInventory(CollectableID.Firelighter) && !_isLightenedUp)
        {
            _fire.SetActive(true);
            _isLightenedUp = true;
            StartCoroutine(PlaySoundsInOrder());
            PlayerInventoryManager.Instance.RemoveFromList(CollectableID.Fuel);
        }
        else if(PlayerInventoryManager.Instance.IsInInventory(CollectableID.Fuel))
        {
            //need firelighter
        }
        else if(PlayerInventoryManager.Instance.IsInInventory(CollectableID.Firelighter))
        {
            //need Fuel
        }
    }
    IEnumerator PlaySoundsInOrder()
    {
        _audio.PlayOneShot(_audioClips[0]);
        yield return new WaitForSeconds(1.2f);
        _audio.clip = _audioClips[1];
        _audio.Play();
        yield return null;
    }
}
