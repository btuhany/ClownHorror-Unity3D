
using UnityEngine;
using Enums;
namespace Mechanics
{
    public class Sound 
    {
        public readonly SoundType Type;
        public readonly LayerMask Layer;
        public readonly Vector3 Pos;
        public readonly float Range;
        public readonly GameObject GameObject;
        public Sound(Vector3 pos, float range, SoundType type, LayerMask layer, GameObject gameObject)
        {
            Layer = layer;
            Type = type;
            Pos = pos;
            Range = range;
            GameObject = gameObject;
        }
    }
}

