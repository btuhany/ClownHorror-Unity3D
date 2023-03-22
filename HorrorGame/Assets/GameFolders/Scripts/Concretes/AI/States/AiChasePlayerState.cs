using UnityEngine;
using Unity;
using AI;
using System.Collections;

namespace AI.States
{
    public class AiChasePlayerState : IAiState
    {
        float _setDestinationTimer;  // Set destination sample tiner
        float _changeStateTimer;
        bool _isPlayerLost;
        
        AiEnemy _ai;

        public AiChasePlayerState(AiEnemy enemy)
        {
            _ai = enemy;
        }
        public AiStateId StateId => AiStateId.ChasePlayer;
        public void Update()   //use functions from AiEnemy?
        {
            if(IsPlayerInSight())
            {
                if (_isPlayerLost) //PlayerFound
                {
                    OnPlayerFound();
                } 

                _setDestinationTimer -= Time.deltaTime;
                if (_setDestinationTimer < 0f)
                {
                    _setDestinationTimer = _ai.Config.MaxSetDestTime;
                    _ai.NavMeshAgent.SetDestination(_ai.PlayerTransform.position);
                }
            }
            else if(IsPlayerHeard())
            {
                if (_isPlayerLost) //PlayerFound
                {
                    OnPlayerFound();
                }
                _ai.NavMeshAgent.SetDestination(_ai.LastHeardSound.Pos);
                
            }
            else if(!_isPlayerLost) //PlayerLost
            {
                OnPlayerLost();

            }


            if (_isPlayerLost)
            {
                _changeStateTimer -= Time.deltaTime;
                if (_changeStateTimer < 0f)
                {
                    _ai.StateMachine.ChangeState(AiStateId.Idle);
                }
            }

        }
        public void Enter()
        {
            OnPlayerFound();
        }

        public void Exit()
        {

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
        private bool IsPlayerHeard()
        {
            if (_ai.LastHeardSound == null) return false;
            if (_ai.LastHeardSound.GameObject.CompareTag("Player"))
            {        
                return true;
            }
            return false;
        }

        void OnPlayerLost()
        {
            _ai.NavMeshAgent.speed = _ai.CurrentMovementSpeeds[1];            
          //  Debug.Log("PlayerLost");
            _isPlayerLost = true;
        }
        void OnPlayerFound()
        {
            _changeStateTimer = _ai.Config.MaxChangeStateTime;
            _ai.NavMeshAgent.speed = _ai.CurrentMovementSpeeds[0];
            //Debug.Log("PlayerFound");
            _isPlayerLost = false;
        }

    }

}
