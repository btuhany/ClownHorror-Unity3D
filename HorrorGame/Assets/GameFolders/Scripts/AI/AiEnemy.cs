using UnityEngine;
using UnityEngine.AI;

public class AiEnemy : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] AiStateId _initialState;
    [SerializeField] AiEnemyConfig _config;
    NavMeshAgent _navMeshAgent;
    
    private AiStateMachine _stateMachine;

    public AiEnemyConfig Config { get => _config; set => _config = value; }
    public NavMeshAgent NavMeshAgent { get => _navMeshAgent; set => _navMeshAgent = value; }
    public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _stateMachine = new AiStateMachine(this);
        _stateMachine.RegisterState(new AiChasePlayerState(this));
    }
    private void OnEnable()
    {
        _stateMachine.ChangeState(_initialState);
    }
    private void Update()
    {
        _stateMachine.Update();
    }
}
