public class AiChasePlayerState : IAiState
{
    AiEnemy _ai;
    public AiChasePlayerState(AiEnemy enemy)
    {
        _ai = enemy;
    }
    public AiStateId StateId => AiStateId.ChasePlayer;
    public void Update()
    {
        _ai.NavMeshAgent.SetDestination(_ai.PlayerTransform.position);
    }
    public void Enter()
    {
        //if (_playerTransform.position==null)
        //{
        //    _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //}
    }

    public void Exit()
    {

    }
}
