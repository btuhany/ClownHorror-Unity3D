using UnityEngine;
using Unity;
using AI;
using System.Collections;
using System;

namespace AI.States
{
    public class AiChasePlayerState : IAiState
    {
        float _setDestinationTimer;  // Set destination sample tiner
        float _chasePlayerTimeout;
        bool _isPlayerLost;
        bool _isNewChase=true;
        float _chaseAfterLostTimer;
        bool _chaseAfterLost;
        AiEnemy _ai;

        public AiChasePlayerState(AiEnemy enemy)
        {
            _ai = enemy;
            _ai.Health.OnStunned += HandleOnStunned;
        }

        private void HandleOnStunned()
        {
            _ai.SoundController.ChaseOver();
            _isNewChase = true;
            SoundManager.Instance.ResetStopHeartbeat();
        }

        public AiStateId StateId => AiStateId.ChasePlayer;

        public bool IsNewChase { get => _isNewChase; set => _isNewChase = value; }

        public void Update()   //use functions from AiEnemy?
        {
            _ai.IsThereDoorOpenIt();
            if (_ai.IsPlayerInSight())
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
                    _ai.SoundController.ChaseOver();
                    _isNewChase = true;
                    SoundManager.Instance.ResetStopHeartbeat();
                    _ai.StateMachine.ChangeState(AiStateId.SeekPlayer);
                }
            }
            if(_chaseAfterLost)
            {
                _chaseAfterLostTimer-=Time.deltaTime;
                if(_chaseAfterLostTimer<0)
                {
                   
                    _ai.NavMeshAgent.SetDestination(_ai.PlayerTransform.position);
                    _chaseAfterLost = false;
                }
            }
            SoundManager.Instance.SetHeartbeatSpeed(10f/Vector3.Distance(_ai.transform.position, _ai.PlayerTransform.position),0.1f);

        }
        public void Enter()
        {
            _chaseAfterLostTimer = 1.2f;
            OnPlayerFound();
            _ai.NavMeshAgent.SetDestination(_ai.PlayerTransform.position);
            
            if(_isNewChase)
            {
                SoundManager.Instance.StartHeartbeatLoop();
                SoundManager.Instance.EnemyActionSounds(0);
                _ai.SoundController.PlayerFound();
                _isNewChase = false;
            }
                
            
            
        }

        public void Exit()
        {
            if(_isNewChase)
                SoundManager.Instance.EnemyActionSounds(1);
            
        }



        void OnPlayerLost()
        {
            _ai.NavMeshAgent.speed = _ai.CurrentMovementSpeeds[3];            
          //  Debug.Log("PlayerLost");
            _isPlayerLost = true;
            _chaseAfterLost = true;

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
