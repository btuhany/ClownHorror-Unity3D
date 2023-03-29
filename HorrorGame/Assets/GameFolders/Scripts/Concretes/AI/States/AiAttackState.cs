using AI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AiAttackState : IAiState
{
    AiEnemy _ai;
    public AiAttackState(AiEnemy ai)
    {
        _ai = ai;
    }

    public AiStateId StateId => AiStateId.Attack;

    public void Enter()
    {
        _ai.Combat.IsAttacking = true;
        _ai.NavMeshAgent.isStopped = true;
        _ai.Anim.SetTrigger("IsAttacked");
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
        LookAtPlayer();
        if(Vector3.Distance(_ai.PlayerTransform.position,_ai.transform.position)>_ai.Config.MaxAttackDistance)
        {
            _ai.StateMachine.ChangeState(AiStateId.ChasePlayer);
        }
        

    }
    void LookAtPlayer()
    {
        Vector3 playerPos = _ai.PlayerTransform.position;
        Vector3 targetPos = new Vector3(playerPos.x, _ai.transform.position.y - 0.2f, playerPos.z);
        _ai.transform.LookAt(targetPos);
    }
}
