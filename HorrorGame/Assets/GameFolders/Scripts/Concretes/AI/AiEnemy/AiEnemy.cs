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
        private SightSensor _sightSensor;
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
            _sightSensor = GetComponent<SightSensor>();
            _anim = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _stateMachine = new AiStateMachine(this);
            _stateMachine.RegisterState(new AiChasePlayerState(this));
            _stateMachine.RegisterState(new AiIdleState(this));
            _stateMachine.RegisterState(new AiWanderState(this));
            _stateMachine.RegisterState(new AiSeekPlayerState(this));
            _stateMachine.RegisterState(new AiGoToPointState(this));
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


        public bool IsPlayerInSight()
        {
            if (_sightSensor.ObjectsInSightList.Count > 0)
            {
  
                foreach (var gameObj in _sightSensor.ObjectsInSightList)
                {
                    if (gameObj.CompareTag("Player"))
                        return true;
                }
                
                
            }
            return false;
        }
        public bool IsPlayerHeard()
        {
            if (LastHeardSound != null)
            {
                if (LastHeardSound.GameObject.CompareTag("Player"))
                {
                    return true;
                }
            }
            return false;
        }
        public Vector3 RandomPointOnNavMesh(Vector3 center, float range, float samplePointRange)
        {
            Vector3 randomVector = Random.insideUnitSphere;
            //randomVector.y = 0f;
            Vector3 randomPoint = center + randomVector * range;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, samplePointRange, NavMesh.AllAreas))
            {
                return hit.position;
            }
            return center;
        }
    }

}
