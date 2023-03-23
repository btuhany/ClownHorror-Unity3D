using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.States
{
    public class AiGoToPointState : IAiState
    {
        AiEnemy _ai;
        Vector3 _tempDestination;
        public AiStateId StateId => AiStateId.GoToPoint;

        public AiGoToPointState(AiEnemy ai)
        {
            _ai = ai;
        }
        public void Enter()
        {
            _ai.NavMeshAgent.speed = _ai.CurrentMovementSpeeds[4];
            _tempDestination = AiWayPoints.Instance.GetRandomPointFromInstance();
            _ai.NavMeshAgent.SetDestination(_tempDestination);
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            
            if (!_ai.NavMeshAgent.hasPath)
            {
                _tempDestination = AiWayPoints.Instance.GetRandomPointFromInstance();
                _ai.NavMeshAgent.SetDestination(_tempDestination);
            }
            
            if (Vector3.Distance(_ai.transform.position, _tempDestination) < 0.2f)
            {
                _ai.StateMachine.ChangeState(AiStateId.SeekPlayer);
            }


        }
    }

}
