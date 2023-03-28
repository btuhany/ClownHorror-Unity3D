using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.States
{
    public class AiStunnedState : IAiState
    {
        public AiStateId StateId => AiStateId.Stunned;

        AiEnemy _ai;

        float _stunTimer;
        public AiStunnedState(AiEnemy enemy)
        {
            _ai = enemy;

        }

        public void Enter()
        {
            _ai.Anim.SetBool("IsStunned",true);
            _stunTimer = _ai.Config.MaxStunTime;
            _ai.NavMeshAgent.SetDestination(_ai.transform.position);  //stop?
        }

        public void Exit()
        {
         
        }

        public void Update()
        {
            _stunTimer -= Time.deltaTime;
            if(_stunTimer<0)
            {
                _ai.StateMachine.ChangeState(AiStateId.Idle);
                _ai.Anim.SetBool("IsStunned", false);
                _ai.Health.IsStunned = false;
            }
        }
    }

}
