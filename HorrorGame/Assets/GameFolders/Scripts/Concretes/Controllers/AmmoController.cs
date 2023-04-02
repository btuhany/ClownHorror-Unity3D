using Abstracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : Collectable
{
    protected override void Collect()
    {
        PlayerInventoryManager.Instance.IncreaseAmmo();
        base.Collect();
    }
}
