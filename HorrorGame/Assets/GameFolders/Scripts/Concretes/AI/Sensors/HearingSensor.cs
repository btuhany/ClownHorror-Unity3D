
using UnityEngine;
using Mechanics;
using AI;

namespace Sensors
{
    public class HearingSensor : MonoBehaviour
    {
        [SerializeField] AiEnemy _ai;

        public void Hear(Sound sound)
        {
            _ai.LastHeardSound = sound;
            Debug.Log(sound.GameObject.name + sound.Type + sound.Pos);
        }

    }

}
