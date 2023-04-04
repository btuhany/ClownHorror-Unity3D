
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolumeController : MonoBehaviour
{
    Volume _volume;
    Vignette _vignet;
    ChromaticAberration _chromatic;
    private void Awake()
    {
        _volume = GetComponent<Volume>();
    }
    public void FXOn()
    {
        if(_volume.profile.TryGet<Vignette>(out _vignet))
        {
            _vignet.active = true;
        }
        if(_volume.profile.TryGet<ChromaticAberration>(out _chromatic))
        {
            _chromatic.active = true;
        }
    }
    public void FXOff()
    {
        if (_volume.profile.TryGet<Vignette>(out _vignet))
        {
            _vignet.active = false;
        }
        if (_volume.profile.TryGet<ChromaticAberration>(out _chromatic))
        {
            _chromatic.active = false;
        }
    }


}
