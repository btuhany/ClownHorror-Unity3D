using Abstracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : SingletonMonoObject<PlayerInventoryManager>
{
    [Header("ItemAcquireSounds")]
    [SerializeField] List<AudioClip> _audioClips = new List<AudioClip>(); 
    [Header("Ammo")]
    [SerializeField] int _ammoIncrement = 5;
    private List<Collectable> _collectableInventory = new List<Collectable>();
    private int _totalAmmo = 0;
    public CollectableID LastChangedItemID;
    public event System.Action OnAmmoChanged;
    public event System.Action OnItemAcquired;
    
    public int TotalAmmo { get => _totalAmmo; }
    public bool IsThereAmmo => _totalAmmo > 0;

    private AudioSource _audio;
    private void Awake()
    {
        SingletonThisObject(this);
        OnItemAcquired += HandleSoundsOnItemAcquired;
    }
    public void AddToList(Collectable collectable)
    {
        LastChangedItemID = collectable.CollectableID;
        _collectableInventory.Add(collectable);
        OnItemAcquired?.Invoke();
    }
    public void RemoveFromList(CollectableID collectable)
    {
        foreach (var item in _collectableInventory)
        {
            if (item.CollectableID == collectable)
            {
                LastChangedItemID = collectable;
                _collectableInventory.Remove(item);
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

    public bool IsInInventory(CollectableID collectable)
    {
        foreach (var item in _collectableInventory)
        {
            if(item.CollectableID == collectable)
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
