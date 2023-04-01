using Abstracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : SingletonMonoObject<PlayerInventoryManager>
{
    [SerializeField] int _ammoIncrement = 6;
    private List<Collectable> _collectableInventory = new List<Collectable>();
    private int _totalAmmo = 0;
    private void Awake()
    {
        SingletonThisObject(this);
    }
    public void AddToList(Collectable collectable)
    {
       
        _collectableInventory.Add(collectable);
    }
    public void IncreaseAmmo()
    {
        _totalAmmo = _totalAmmo + _ammoIncrement;
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
}
