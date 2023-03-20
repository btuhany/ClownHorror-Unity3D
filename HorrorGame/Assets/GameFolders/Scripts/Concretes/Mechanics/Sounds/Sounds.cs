using UnityEngine;

namespace Mechanics
{
    public static class Sounds
    {
        public static void CreateWaves(Sound sound)
        {
            Collider[] colliders = Physics.OverlapSphere(sound.Pos, sound.Range, sound.Layer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out HearingSensor listener))
                {
                    listener.Hear(sound);
                }
            }
        }
    }
}

