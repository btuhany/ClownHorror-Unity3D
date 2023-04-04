using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.States
{
    public class AiCheckNoise : IAiState
    {
        AiEnemy _ai;
        public AiStateId StateId => AiStateId.CheckNoise;
        public AiCheckNoise(AiEnemy enemy)
        {
            _ai = enemy;

        }
       

        public void Enter()
        {
            _ai.NavMeshAgent.speed = _ai.CurrentMovementSpeeds[1];
            _ai.NavMeshAgent.ResetPath();
            _ai.NavMeshAgent.SetDestination(_ai.LastHeardSoundPos);
            _ai.SoundController.Rotate();
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            _ai.NavMeshAgent.SetDestination(_ai.LastHeardSoundPos);  // enter() function is not sufficient in some cases.
            if(!_ai.NavMeshAgent.hasPath)
            {
                _ai.StateMachine.ChangeState(AiStateId.SeekPlayer);
            }


        }
    }

}
