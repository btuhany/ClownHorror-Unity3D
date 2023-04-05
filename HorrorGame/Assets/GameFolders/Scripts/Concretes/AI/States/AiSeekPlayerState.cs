using AI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.States
{
    public class AiSeekPlayerState : IAiState
    {
        bool _isRotated;
        float _timeOutForPotantialBug;
        float _delayAfterRotate;
        float _rotationTimer;
        float _forwardDistance;
        float _seekTimeout;
        Vector3 _tempDestination;

        Vector3 _rotationVector3;
        AiEnemy _ai;
        public AiSeekPlayerState(AiEnemy ai)
        {
            _ai = ai;
        }

        public AiStateId StateId => AiStateId.SeekPlayer;

        public void Enter()
        {
            _ai.SoundController.Rotate();
            _ai.NavMeshAgent.ResetPath();
            _ai.NavMeshAgent.speed = _ai.CurrentMovementSpeeds[2];
            _rotationTimer = _ai.Config.MaxRotationTime;
            _tempDestination = _ai.transform.position;
            _delayAfterRotate = _ai.Config.DelayAfterRotate;
            _forwardDistance = _ai.Config.SeekForwardDistance;
            _seekTimeout = _ai.Config.SeekTimeOut;
            _isRotated = true;
            _timeOutForPotantialBug = 15f;
        }

        public void Exit()
        {
            _ai.NavMeshAgent.ResetPath();
            _ai.Anim.ResetTrigger("rotateLeft");
            _ai.Anim.ResetTrigger("rotateRight");
        }

        public void Update()
        {
            _timeOutForPotantialBug -= Time.deltaTime;
            if(_timeOutForPotantialBug<0f)
            {
                _timeOutForPotantialBug = 15f;
                _ai.StateMachine.ChangeState(AiStateId.Wander);
            }
            _seekTimeout -= Time.deltaTime;
            if (_ai.IsPlayerInSight() || _ai.IsPlayerHeard())
                _ai.StateMachine.ChangeState(AiStateId.ChasePlayer);

     
            
            
            Debug.Log(_delayAfterRotate );
            Debug.Log(_rotationTimer );
            Debug.Log( _forwardDistance );
            Debug.Log( _seekTimeout);


            if (_isRotated)
            {
                Debug.Log("123");
                _delayAfterRotate -= Time.deltaTime;
                if (_delayAfterRotate < 0f)
                {
                    Debug.Log("1234");
                    if (_seekTimeout < 0)  // dont change state while rotating
                    {
                        
                        _ai.StateMachine.ChangeState(AiStateId.Wander);
                    }
             
                    GoForwardAtRandomDistance();
                    _delayAfterRotate = _ai.Config.DelayAfterRotate;
                    _isRotated = false;
                } 
            }
            else if(_rotationTimer < 0f)
            {
                Debug.Log("1");
                int randomDirectionIndex = Random.Range(1, 4);
                float randomEuler = Random.Range(5, 91);
                float oldRotation = _ai.transform.rotation.y;
                float newRotationY = _ai.transform.rotation.eulerAngles.y + randomEuler * randomDirectionIndex;
                if (newRotationY > 350f) newRotationY = 0f;
                _rotationVector3 = new Vector3(0, newRotationY, 0);
                _ai.transform.DORotateQuaternion(Quaternion.Euler(_rotationVector3), 2);
                _rotationTimer = _ai.Config.MaxRotationTime;
                _isRotated = true;

                if (newRotationY < oldRotation) //could be better
                    _ai.Anim.SetTrigger("rotateLeft");
                else
                    _ai.Anim.SetTrigger("rotateRight");
                _ai.SoundController.Rotate();
            }
            else if (Vector3.Distance(_ai.transform.position, _tempDestination) < 0.3f)
            {
                Debug.Log("12");
                _rotationTimer -= Time.deltaTime;
            }
            if (_ai.IsHeardSomething())
                _ai.StateMachine.ChangeState(AiStateId.CheckNoise);
        }        
        private Vector3 ForwardPointOnNavmesh(float distance,float samplePointRange)
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
            Vector3 randomPos = ForwardPointOnNavmesh(_forwardDistance, _ai.Config.SeekRandomSamplePointRange);
            _tempDestination = randomPos;
            _ai.NavMeshAgent.SetDestination(randomPos);
        }
    }
}


