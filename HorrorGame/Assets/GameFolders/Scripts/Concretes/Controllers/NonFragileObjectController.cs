using Abstracts;
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
            CreateTheSoundWave();
        }
    }
}
