
using UnityEngine;
using Mechanics;
using AI;
public class HearingSensor : MonoBehaviour
{
    AiEnemy _ai;

    private void Awake()
    {
        _ai = GetComponent<AiEnemy>();
    }

    public void Hear(Sound sound)
    {
        _ai.LastHeardSound= sound;
        // Debug.Log(sound.GameObject.name + sound.Type + sound.Pos);
    }

}
