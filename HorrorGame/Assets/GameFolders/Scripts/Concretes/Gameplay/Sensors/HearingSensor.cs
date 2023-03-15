using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingSensor : MonoBehaviour
{
    public bool dontListen;
    private void Update()
    {
        if (!dontListen)
            Sounds.DontListen = false;
        else
            Sounds.DontListen = true;
    }
    public void Hear(Sound sound)
    {
        Debug.Log(gameObject.name + sound.Type + sound.Pos);

    }
}
