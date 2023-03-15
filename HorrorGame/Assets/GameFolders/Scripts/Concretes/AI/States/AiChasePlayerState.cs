using UnityEngine;
public class AiChasePlayerState : IAiState
{
    float _timer;
    AiEnemy _ai;
    public AiChasePlayerState(AiEnemy enemy)
    {
        _ai = enemy;
    }
    public AiStateId StateId => AiStateId.ChasePlayer;
    public void Update()   //use functions from AiEnemy?
    {
        _timer -= Time.deltaTime;
        if(_timer < 0f)
        {
            _timer = _ai.Config.MaxSetDestTime;
            _ai.NavMeshAgent.SetDestination(_ai.PlayerTransform.position);
        }
    }
    public void Enter()
    {
        
    }

    public void Exit()
    {

    }
}
