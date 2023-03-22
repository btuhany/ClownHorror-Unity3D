using AI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.States
{
    public class AiSeekPlayerState : IAiState
    {
        bool _isRotated = false;
        float _rotationTimer;
        float _setDestTimer = -1;

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
        }

        public void Exit()
        {

        }

        public void Update()
        {
            if (_ai.IsPlayerInSight() || _ai.IsPlayerHeard())
                _ai.StateMachine.ChangeState(AiStateId.ChasePlayer);
            
                _rotationTimer -= Time.deltaTime;
                if (_rotationTimer < 0f)
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
                    _setDestTimer = 2f;
                    if (newRotationY < oldRotation) //could be better
                        _ai.Anim.SetTrigger("rotateLeft");
                    else
                        _ai.Anim.SetTrigger("rotateRight");
                }
                
        }

                


    }

}


