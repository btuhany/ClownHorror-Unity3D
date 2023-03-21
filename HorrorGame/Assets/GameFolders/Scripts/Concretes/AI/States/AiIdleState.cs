using AI;
using DG.Tweening;
using System.Collections;
using System.Net.Sockets;
using Unity;
using UnityEngine;

namespace AI.States
{
    public class AiIdleState : IAiState
    {
        AiEnemy _ai;
        Animator _anim;
   
        public AiStateId StateId => AiStateId.Idle;
        public AiIdleState(AiEnemy enemy)
        {
            _ai = enemy;
            _anim = _ai.Anim;
        }
        float _timer = 0;
        int _counter= 0;
        public void Enter()
        {

        }

        public void Exit()
        {

        }

        public void Update()
        {
            
            CheckPlayerInSight();
            CheckEars();
            _timer += Time.deltaTime;
            if(_timer>3)
            {
                if(_counter==0 )
                {
                    _ai.transform.DORotate(new Vector3(0,90,0), 2.3f);

                    _counter=2;
                    
                }
                
                else if (_counter == 2)
                {
                    _ai.transform.DORotate(new Vector3(0, 270, 0), 2.3f);
                    _counter = 0;

                }

                _anim.SetTrigger("rotateRight");
                _timer = 0;
            }
            
            
        }

        private void CheckEars()
        {
            if (_ai.LastHeardSound != null)
            {
                _ai.NavMeshAgent.SetDestination(_ai.LastHeardSound.Pos);
                _ai.LastHeardSound = null;
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
        
    }

}
