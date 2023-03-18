using System;
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
        // CheckPlayerInSight();
        CheckEars();
    }

    private void CheckEars()
    {
        if(_ai.LastHeardSound!= null)
        {
            _ai.NavMeshAgent.SetDestination(_ai.LastHeardSound.Pos);
            _ai.LastHeardSound=null;
        }
    }

    private bool IsPlayerInSight()
    {
        foreach (var gameObj in _ai.SightSensor.ObjectsInSightList)
        {
            if (gameObj.CompareTag("Player"))
                return true;
        }
        return false;
    }
    private void CheckPlayerInSight()
    {
        if (_ai.SightSensor.ObjectsInSightList.Count > 0)
        {
            if (IsPlayerInSight())
            {
                _ai.StateMachine.ChangeState(AiStateId.ChasePlayer);
            }
        }
    }
}
