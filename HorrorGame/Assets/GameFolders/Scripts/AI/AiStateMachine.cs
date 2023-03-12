using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateMachine 
{
    public AiState[] States;
    public AiEnemy Enemy;
    public AiStateId CurrentState;
    public AiStateMachine(AiEnemy enemy)
    {
        Enemy = enemy;
        int numStates = System.Enum.GetNames(typeof(AiStateId)).Length;
        States = new AiState[numStates];
    }
    public void RegisterState(AiState state)
    {
        int index = (int)state.GetId();
        States[index] = state;
    }
    public AiState GetState(AiStateId stateId)
    {
        int index = (int)stateId;
        return States[index];
    }
    public void Update()
    {
        GetState(CurrentState)?.Update(Enemy);
    }
    public void ChangeState(AiStateId newState)
    {
        GetState(CurrentState)?.Exit(Enemy);
        CurrentState = newState;
        GetState(CurrentState)?.Enter(Enemy);
    }
}
