using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using UnityEngine.AI;

namespace AI.States
{
    public class AiWanderState : IAiState
    {
        AiEnemy _ai;
        NavMeshAgent _navmesh;
        public AiStateId StateId => AiStateId.Wander;

        public AiWanderState(AiEnemy aiEnemy)
        {
            _ai = aiEnemy;
            
        }
        public void Update()
        {
           if(!_ai.NavMeshAgent.hasPath)
           {
                _ai.NavMeshAgent.SetDestination(_ai.RandomPointOnNavMesh(_ai.transform.position, _ai.Config.WanderRandomPointRadius,_ai.Config.WanderRandomSamplePointRange));
           }
            if (_ai.IsPlayerInSight() || _ai.IsPlayerHeard())
                _ai.StateMachine.ChangeState(AiStateId.ChasePlayer);

        }

        public void Enter()
        {
            _ai.NavMeshAgent.speed = _ai.CurrentMovementSpeeds[4];
        }

        public void Exit()
        {
            
        }

  
    }
}

