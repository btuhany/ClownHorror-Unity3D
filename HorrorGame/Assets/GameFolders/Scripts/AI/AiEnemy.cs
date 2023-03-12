using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AiStateMachine))]
public class AiEnemy : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] AiStateId _initialState;
    [SerializeField] AiEnemyConfig _config;
    AiStateMachine _stateMachine;
    NavMeshAgent _navMeshAgent;

    public AiEnemyConfig Config { get => _config; }
    public Transform PlayerTransform { get => _playerTransform; }
    public NavMeshAgent NavMeshAgent { get => _navMeshAgent; }

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
