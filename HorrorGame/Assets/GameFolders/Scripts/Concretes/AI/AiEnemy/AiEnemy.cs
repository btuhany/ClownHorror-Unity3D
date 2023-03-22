using UnityEngine;
using UnityEngine.AI;
using Mechanics;
using AI.States;
using TMPro;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AiStateMachine))]
    public class AiEnemy : MonoBehaviour
    {
        [SerializeField] Transform _playerTransform;
        [SerializeField] AiStateId _initialState;
        [SerializeField] AiEnemyConfig _config;
        AiEnemyDifficulties _currentDifficulty;
        AiStateMachine _stateMachine;
        NavMeshAgent _navMeshAgent;
        Animator _anim;
        public float[] CurrentMovementSpeeds;
        public SightSensor SightSensor;
        public Sound LastHeardSound;


        public AiEnemyConfig Config { get => _config; }
        public Transform PlayerTransform { get => _playerTransform; }
        public NavMeshAgent NavMeshAgent { get => _navMeshAgent; }
        public AiStateMachine StateMachine { get => _stateMachine; set => _stateMachine = value; }
        public Animator Anim { get => _anim; set => _anim = value; }

        private void Awake()
        {
            _currentDifficulty = AiEnemyDifficulties.Easy;
            CurrentMovementSpeeds = _config.EasyMovementSpeeds;
            SightSensor = GetComponent<SightSensor>();
            _anim = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _stateMachine = new AiStateMachine(this);
            _stateMachine.RegisterState(new AiChasePlayerState(this));
            _stateMachine.RegisterState(new AiIdleState(this));
            _stateMachine.RegisterState(new AiWanderState(this));
        }
        private void OnEnable()
        {
            _stateMachine.ChangeState(_initialState);
        }
        private void Update()
        {
            _stateMachine.Update();
            if(LastHeardSound!= null)  //LastHeardProcesses in stateMachine Updates
            {
                LastHeardSound = null;
            }
        }
        public void ChangeDifficulty(AiEnemyDifficulties newDifficulty)
        {
            _currentDifficulty = newDifficulty;
            if(_currentDifficulty == AiEnemyDifficulties.Easy)
            {
                CurrentMovementSpeeds = _config.EasyMovementSpeeds;
            }
            else if(_currentDifficulty == AiEnemyDifficulties.Normal)
            {
                CurrentMovementSpeeds = _config.NormalMovementSpeeds;
            }
            else if(_currentDifficulty == AiEnemyDifficulties.Hard)
            {
                CurrentMovementSpeeds = _config.HardMovementSpeeds;
            }   
        }

    }

}
