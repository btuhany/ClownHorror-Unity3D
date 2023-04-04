using Abstracts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerInventoryManager : SingletonMonoObject<PlayerInventoryManager>
{
    [Header("ItemAcquireSounds")]
    [SerializeField] List<AudioClip> _audioClips = new List<AudioClip>(); 
    [Header("Ammo")]
    [SerializeField] int _ammoIncrement = 5;
     List<CollectableID> _collectableInventory = new List<CollectableID>();
    private int _totalAmmo = 0;
    public CollectableID LastChangedItemID;
    public event System.Action OnAmmoChanged;
    public event System.Action OnItemAcquired;
    public event System.Action OnItemRemoved;
    
    public int TotalAmmo { get => _totalAmmo; }
    public bool IsThereAmmo => _totalAmmo > 0;

    private AudioSource _audio;
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        SingletonThisObject(this);
        
    }
    private void OnEnable()
    {
        GameManager.Instance.OnGameRestart += HandleOnRestart;
        OnItemAcquired += HandleSoundsOnItemAcquired;
    }
    private void HandleOnRestart()
    {
        _collectableInventory.Clear();
        LastChangedItemID = CollectableID.KeyBlue;
        _totalAmmo = 0;
    }
    public void AddToList(Collectable collectable)
    {
        LastChangedItemID = collectable.CollectableID;
        _collectableInventory.Add(collectable.CollectableID);
        OnItemAcquired?.Invoke();
    }
    public void RemoveFromList(CollectableID collectableID)
    {
        foreach (var item in _collectableInventory)
        {
            if (item == collectableID)
            {
                LastChangedItemID = collectableID;
                _collectableInventory.Remove(item);
                OnItemRemoved?.Invoke();
                break;
            }
        }
        
    }
    public void IncreaseAmmo()
    {
        _totalAmmo = _totalAmmo + _ammoIncrement;
        _audio.PlayOneShot(_audioClips[3]);
        OnAmmoChanged?.Invoke();
    }
    public void DecreaseAmmo(int number)
    {
        _totalAmmo -= number; 
        OnAmmoChanged?.Invoke();
    }

    public bool IsInInventory(CollectableID collectableID)
    {
        foreach (var item in _collectableInventory)
        {
            if(item == collectableID)
            {
                return true;
            }
        }
        return false;
    }
    private void HandleSoundsOnItemAcquired()
    {
        switch (LastChangedItemID)
        {
            case CollectableID.KeyBlue:
                _audio.PlayOneShot(_audioClips[0]);          
                break;
            case CollectableID.KeyGreen:
                _audio.PlayOneShot(_audioClips[0]);
                break;
            case CollectableID.KeyRed:
                _audio.PlayOneShot(_audioClips[0]);
                break;
            case CollectableID.KeyBlack:
                _audio.PlayOneShot(_audioClips[0]);
                break;
            case CollectableID.Fuel:
                _audio.PlayOneShot(_audioClips[1]);
                break;
            case CollectableID.Firelighter:
                _audio.PlayOneShot(_audioClips[2]);
                break;
        }
    }
}
