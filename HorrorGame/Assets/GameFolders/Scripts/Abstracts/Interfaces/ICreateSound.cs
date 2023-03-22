using Enums;
using UnityEngine;

public interface ICreateSound
{
   void CreateSoundWaves(float range, SoundType soundType, LayerMask layer, GameObject gameObj);
}
