using UnityEngine;

namespace AI.States
{
    public class AiIdleState : IAiState
    {
        AiEnemy _ai;
        private float _idleWaitTime;
   
        public AiStateId StateId => AiStateId.Idle;
        public AiIdleState(AiEnemy enemy)
        {
            _ai = enemy;
           
        }

        public void Enter()
        {
            _idleWaitTime = _ai.Config.IdleWaitTime;
        }

        public void Exit()
        {

        }

        public void Update()
        {
            _idleWaitTime -= Time.deltaTime;  
            if (_idleWaitTime < 0)
            {
                _ai.StateMachine.ChangeState(AiStateId.Wander);
            }
            if (_ai.IsPlayerInSight() || _ai.IsPlayerHeard())
                _ai.StateMachine.ChangeState(AiStateId.ChasePlayer);

        }

    }

}
