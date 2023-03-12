using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiEnemy : MonoBehaviour
{
    private AiStateMachine _stateMachine;
    public AiStateId InitialState;
    private void Awake()
    {
        _stateMachine = new AiStateMachine(this);
    }
    private void OnEnable()
    {
        _stateMachine.ChangeState(InitialState);
    }
    private void Update()
    {
        _stateMachine.Update();
    }
}
