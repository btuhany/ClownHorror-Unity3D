using AI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AiAttackState : IAiState
{
    AiEnemy _ai;
    float _timeCounter=2f;
    float _soundTimeCounter=2f;
    public AiAttackState(AiEnemy ai)
    {
        _ai = ai;
    }

    public AiStateId StateId => AiStateId.Attack;

    public void Enter()
    {
        _ai.SoundController.AttackLaugh();
        _ai.Combat.IsAttacking = true;
        _ai.NavMeshAgent.isStopped = true;
        _ai.NavMeshAgent.ResetPath();
        _ai.Anim.SetTrigger("IsAttacked");
        _soundTimeCounter = 2f;
        _timeCounter = 1.2f; 
    }

    public void Exit()
    {
        _ai.Combat.IsAttacking = false;
        _ai.NavMeshAgent.isStopped = false;
        _ai.Anim.ResetTrigger("IsAttacked");
    }

    public void Update()
    {
        _ai.Anim.SetTrigger("IsAttacked");
        _soundTimeCounter -=Time.deltaTime;
        if(_soundTimeCounter<0)
        {
            _soundTimeCounter = 2f;
            _ai.SoundController.AttackLaugh();
        }
        
        if (Vector3.Distance(_ai.PlayerTransform.position, _ai.transform.position) > 5f)
        {
            LookAtPlayer();
            
        }
        if (Vector3.Distance(_ai.PlayerTransform.position,_ai.transform.position)>_ai.Config.MaxAttackDistance)
        {
            Debug.Log(Vector3.Distance(_ai.PlayerTransform.position, _ai.transform.position));
            _timeCounter -= Time.deltaTime;
            if(_timeCounter < 0 )
            {
                _timeCounter = 1.2f;
                _ai.StateMachine.ChangeState(AiStateId.ChasePlayer);
            }
            
        }
        else
        {
            _timeCounter = 1.2f;
        }
        
    }
    void LookAtPlayer()
    {
        Vector3 playerPos = _ai.PlayerTransform.position;
        Vector3 targetPos = new Vector3(playerPos.x, _ai.transform.position.y - 1f, playerPos.z);
        _ai.transform.LookAt(targetPos);
    }
}
