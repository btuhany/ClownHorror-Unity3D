using UnityEngine;

public static class Sounds 
{
    public static bool DontListen;
    public static void CreateWaves(Sound sound)
    {
        if (DontListen) return;
        Collider[] colliders = Physics.OverlapSphere(sound.Pos, sound.Range,sound.Layer);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out HearingSensor listener))
            {
                listener.Hear(sound);
            }
        }
    }
}
