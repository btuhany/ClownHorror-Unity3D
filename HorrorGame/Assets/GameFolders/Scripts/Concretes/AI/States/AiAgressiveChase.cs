using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgressiveChase : IAiState
{
    AiEnemy _ai;
    public AiAgressiveChase(AiEnemy ai)
    {
        _ai = ai;
    }

    public AiStateId StateId => AiStateId.AggressiveChase;

    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
       _ai.NavMeshAgent.SetDestination(_ai.PlayerTransform.position);
    }
}
