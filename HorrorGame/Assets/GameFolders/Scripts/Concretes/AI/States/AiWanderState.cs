using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using UnityEngine.AI;
using Unity.VisualScripting;

namespace AI.States
{
    public class AiWanderState : IAiState
    {
        AiEnemy _ai;
        Vector3 gizmoPos;
        Vector3 _tempDestination;
        float _forwardDistance;
        public AiStateId StateId => AiStateId.Wander;

        public AiWanderState(AiEnemy aiEnemy)
        {
            _ai = aiEnemy;
            
            
        }
        public void Update()
        {
            if (_ai.IsPlayerInSight() || _ai.IsPlayerHeard())
                _ai.StateMachine.ChangeState(AiStateId.ChasePlayer);

 

           if (Vector3.Distance(_ai.transform.position, _tempDestination) < 0.3f || !_ai.NavMeshAgent.hasPath) 
            {
                int randomNumber = Random.Range(1, 101);
                if (randomNumber < 40)
                {
                    _tempDestination = _ai.RandomPointOnNavMesh(_ai.transform.position, _ai.Config.WanderRandomPointRadius, _ai.Config.WanderRandomSamplePointRange);
                    _ai.NavMeshAgent.SetDestination(_tempDestination);
                }
                else if (randomNumber >= 40)
                {
                    
                    GoForwardAtRandomDistance();
                }

      
           }
            if (_ai.IsHeardSomething())
                _ai.StateMachine.ChangeState(AiStateId.CheckNoise);

        }

        public void Enter()
        {
            _ai.NavMeshAgent.speed = _ai.CurrentMovementSpeeds[1];
            _forwardDistance = _ai.Config.WanderForwardDistance;
            _tempDestination = _ai.transform.position;
        }

        public void Exit()
        {
            
        }

        private Vector3 ForwardPointOnNavmesh(float distance, float samplePointRange)
        {
            Vector3 forwardPoint = _ai.transform.position + _ai.transform.forward * distance;
            NavMeshHit hit;
            
            
            if (NavMesh.SamplePosition(forwardPoint, out hit, samplePointRange, NavMesh.AllAreas))
            {
                return hit.position;
            }
            return _ai.transform.position;
        }
        void GoForwardAtRandomDistance()
        {
            Vector3 randomPos = ForwardPointOnNavmesh(_forwardDistance, _ai.Config.WanderRandomSamplePointRange);
            _tempDestination = randomPos;
            _ai.NavMeshAgent.SetDestination(randomPos);
        }


    }
}

