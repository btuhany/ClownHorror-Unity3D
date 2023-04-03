using UnityEngine;
using Unity;
using AI;
using System.Collections;

namespace AI.States
{
    public class AiChasePlayerState : IAiState
    {
        float _setDestinationTimer;  // Set destination sample tiner
        float _chasePlayerTimeout;
        bool _isPlayerLost;
        
        AiEnemy _ai;

        public AiChasePlayerState(AiEnemy enemy)
        {
            _ai = enemy;
        }
        public AiStateId StateId => AiStateId.ChasePlayer;
        public void Update()   //use functions from AiEnemy?
        {
            if(_ai.IsPlayerInSight())
            {
                if (Vector3.Distance(_ai.transform.position, _ai.NavMeshAgent.destination) < _ai.Config.MaxAttackDistance)
                {
                    _ai.StateMachine.ChangeState(AiStateId.Attack);
                }

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
            else if(_ai.IsPlayerHeard())
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


            if (_isPlayerLost && !_ai.NavMeshAgent.hasPath)
            {
                _chasePlayerTimeout -= Time.deltaTime;
                if (_chasePlayerTimeout < 0f)
                {
                   
                    _ai.StateMachine.ChangeState(AiStateId.SeekPlayer);
                }
            }

        }
        public void Enter()
        {
            OnPlayerFound();
            _ai.NavMeshAgent.SetDestination(_ai.PlayerTransform.position);
            SoundManager.Instance.EnemyActionSounds(0);
            _ai.SoundController.PlayerFound();
            
        }

        public void Exit()
        {
            SoundManager.Instance.EnemyActionSounds(1);
            _ai.SoundController.ChaseOver();
        }



        void OnPlayerLost()
        {
            _ai.NavMeshAgent.speed = _ai.CurrentMovementSpeeds[3];            
          //  Debug.Log("PlayerLost");
            _isPlayerLost = true;
            
        }
        void OnPlayerFound()
        {
            _chasePlayerTimeout = _ai.ChaseStateTimeout;
            _ai.NavMeshAgent.speed = _ai.CurrentMovementSpeeds[2];
            _setDestinationTimer = 0f;
            //Debug.Log("PlayerFound");
            _isPlayerLost = false;
            
        }

    }

}
