using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : IAiState
{
    AiEnemy _ai;
    public AiStateId StateId => AiStateId.Idle;
    public AiIdleState(AiEnemy enemy)
    {
        _ai = enemy;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        //change
        float distance = (_ai.PlayerTransform.position -  _ai.transform.position).magnitude;
        if(distance<10f)
        {
            _ai.StateMachine.ChangeState(AiStateId.ChasePlayer);
        }
    }
}
