using Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [SerializeField] PlayerSoundController _sound;

    public void OnNoAmmoAnimation()
    {
        _sound.PlayNoAmmo();
    }
}
