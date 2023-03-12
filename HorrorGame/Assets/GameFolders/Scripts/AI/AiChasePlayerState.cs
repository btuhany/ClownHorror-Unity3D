using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiChasePlayerState : AiState
{
    [SerializeField] Transform _playerTransform;
    NavMeshAgent _agent;
    AiEnemyConfig _config;
    AiEnemy _enemy;
    public AiChasePlayerState(AiEnemy enemy)
    {
        _enemy = enemy;
        _config = enemy.Config;
        _agent = enemy.NavMeshAgent;
    }
    public AiStateId GetId()
    {
        return AiStateId.ChasePlayer;
    }
    public void Enter(AiEnemy enemy)
    {
        //if (_playerTransform.position==null)
        //{
        //    _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //}
    }

    public void Exit(AiEnemy enemy)
    {
        
    }


    public void Update()
    {


            _agent.SetDestination(_enemy.PlayerTransform.position);
        
    }
}
