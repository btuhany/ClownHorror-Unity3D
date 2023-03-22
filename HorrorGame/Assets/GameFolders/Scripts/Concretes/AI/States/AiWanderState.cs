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
                _ai.NavMeshAgent.SetDestination(RandomPoint(_ai.transform.position, _ai.Config.RandomPointRadius));
           }
            CheckPlayerInSight();
            CheckEars();
        }

        public void Enter()
        {
            _ai.NavMeshAgent.speed = _ai.CurrentMovementSpeeds[4];
        }

        public void Exit()
        {
            
        }
        private Vector3 RandomPoint(Vector3 center, float range)
        {
            Vector3 randomVector = Random.insideUnitSphere;
            //randomVector.y = 0f;
            Vector3 randomPoint = center + randomVector * range;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas))
            {
                return hit.position;
            }
            return center;  
        }
        private void CheckEars()
        {
            if (_ai.LastHeardSound != null)
            {
                _ai.NavMeshAgent.SetDestination(_ai.LastHeardSound.Pos);
                _ai.LastHeardSound = null;
            }
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
        private bool IsPlayerInSight()
        {
            foreach (var gameObj in _ai.SightSensor.ObjectsInSightList)
            {
                if (gameObj.CompareTag("Player"))
                    return true;
            }
            return false;
        }
    }
}

