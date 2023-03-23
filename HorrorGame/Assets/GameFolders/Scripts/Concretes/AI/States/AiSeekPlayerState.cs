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

        float _delayAfterRotate;
        float _rotationTimer;
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
            _rotationTimer = _ai.Config.MaxRotationTime;
            _tempDestination = _ai.transform.position;
            _delayAfterRotate = _ai.Config.DelayAfterRotate;
        }

        public void Exit()
        {

        }

        public void Update()
        {
            if (_ai.IsPlayerInSight() || _ai.IsPlayerHeard())
                _ai.StateMachine.ChangeState(AiStateId.ChasePlayer);

                 
            if (_isRotated)
            {
                
                _delayAfterRotate -= Time.deltaTime;
                if (_delayAfterRotate < 0f)
                {
                    GoForwardAtRandomDistance();
                    _delayAfterRotate = _ai.Config.DelayAfterRotate;
                    _isRotated = false;
                } 
            }
            else if(_rotationTimer < 0f)
            {
                
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
            }
            else if (Vector3.Distance(_ai.transform.position, _tempDestination) < 0.2f)
            {
                
                _rotationTimer -= Time.deltaTime;
            }

        }        
        private Vector3 ForwardPointOnNavmesh(float distance,float samplePointRange)
        {
            Vector3 randomPoint = _ai.transform.position + _ai.transform.forward * distance; 
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, samplePointRange, NavMesh.AllAreas))
            {
                return hit.position;
            }
            return _ai.transform.position;
        }
        void GoForwardAtRandomDistance()
        {
            
            Vector3 randomPos = ForwardPointOnNavmesh(3f, _ai.Config.SeekRandomSamplePointRange);
            _tempDestination = randomPos;
            _ai.NavMeshAgent.SetDestination(randomPos);

        }

    }

}


