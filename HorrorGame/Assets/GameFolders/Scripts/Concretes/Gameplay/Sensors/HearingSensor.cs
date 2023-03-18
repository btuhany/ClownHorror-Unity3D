using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log(gameObject.name + sound.Type + sound.Pos);
    }
}
