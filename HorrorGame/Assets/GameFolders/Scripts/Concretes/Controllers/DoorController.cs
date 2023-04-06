using Abstracts;
using AI;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DoorController : Interactable
{
    
    [SerializeField] AudioClip _unlockAudio;
    [SerializeField] AudioClip _openAudio;
    [SerializeField] AudioClip _closeAudio;
    [SerializeField] AudioClip _lockedDoor;
    [SerializeField] private CollectableID _requirementItem;
    [SerializeField] InfoTextUpdater _infoUpdate;
    Vector3 _closedPos;
    AudioSource _audio;
    NavMeshObstacle _navmeshObs;
    bool _isOpened;
    bool _isPlaying;
    bool _isUnlocked;
    private void Awake()
    {
        _audio= GetComponent<AudioSource>();
        _closedPos = transform.rotation.eulerAngles;
        _navmeshObs = GetComponentInChildren<NavMeshObstacle>();
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
        else
        {
            _infoUpdate.DoorLocked(_requirementItem);
            _audio.PlayOneShot(_lockedDoor);
        }
    }
    private void OpenOrClose()
    {
        if(_isPlaying) { return; }
        if(_isOpened)
        {
            _navmeshObs.carving = true;
            _audio.PlayOneShot(_closeAudio);
            transform.DOLocalRotate(_closedPos, 0.3f);
            StartCoroutine(SetIsOpened(false, 0.3f));
           
        }
        else
        {
            _navmeshObs.carving = false;
            _audio.PlayOneShot(_openAudio);
            if(_closedPos.y == 90f)
            {
                transform.DOLocalRotate(new Vector3(0, 0, 0), 2.5f);
            }
            else
                transform.DOLocalRotate(new Vector3(0, 90, 0), 2.5f);
            StartCoroutine(SetIsOpened(true,2.5f));
        }
    }
    public void OpenIfClosedAndUnlocked()
    {
        if (_isPlaying) { return; }
        if (!_isOpened && _isUnlocked)
        {
            _navmeshObs.carving = false;
            _audio.PlayOneShot(_openAudio);
            if (_closedPos.y == 90f)
            {
                transform.DOLocalRotate(new Vector3(0, 0, 0), 2.5f);
            }
            else
                transform.DOLocalRotate(new Vector3(0, 90, 0), 2.5f);
            StartCoroutine(SetIsOpened(true, 2.5f));
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
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Enemy") && _isUnlocked && !_isOpened)
        {
            OpenOrClose();
        }
    }
}
