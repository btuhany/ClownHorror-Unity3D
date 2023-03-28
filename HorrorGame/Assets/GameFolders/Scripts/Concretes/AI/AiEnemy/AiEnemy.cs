using UnityEngine;
using UnityEngine.AI;
using Mechanics;
using AI.States;
using Sensors;
using Controllers;
using System.Collections;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AiStateMachine))]
    public class AiEnemy : MonoBehaviour
    {
        [SerializeField] Transform _playerTransform;
        [SerializeField] AiStateId _initialState;
        [SerializeField] AiEnemyConfig _config;
        [SerializeField] EnemyCombatController _combat;
        AiEnemyDifficulties _currentDifficulty;
        AiStateMachine _stateMachine;
        NavMeshAgent _navMeshAgent;
        EnemyHealthController _health;
        Animator _anim;
        [HideInInspector] public float[] CurrentMovementSpeeds;
        
        private SightSensor _sightSensor;
        public Sound LastHeardSound;


        public AiEnemyConfig Config { get => _config; }
        public Transform PlayerTransform { get => _playerTransform; }
        public NavMeshAgent NavMeshAgent { get => _navMeshAgent; }
        public AiStateMachine StateMachine { get => _stateMachine; set => _stateMachine = value; }
        public Animator Anim { get => _anim; set => _anim = value; }
        public EnemyHealthController Health { get => _health; set => _health = value; }
        public EnemyCombatController Combat { get => _combat; set => _combat = value; }

        private void Awake()
        {
            _currentDifficulty = AiEnemyDifficulties.Easy;
            CurrentMovementSpeeds = _config.EasyMovementSpeeds;
            _sightSensor = GetComponent<SightSensor>();
            _anim = GetComponent<Animator>();
            _health = GetComponent<EnemyHealthController>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _stateMachine = new AiStateMachine(this);
            _stateMachine.RegisterState(new AiChasePlayerState(this));
            _stateMachine.RegisterState(new AiIdleState(this));
            _stateMachine.RegisterState(new AiWanderState(this));
            _stateMachine.RegisterState(new AiSeekPlayerState(this));
            _stateMachine.RegisterState(new AiGoToPointState(this));
            _stateMachine.RegisterState(new AiStunnedState(this));
        }
        private void OnEnable()
        {
            _health.OnStunned += HandleOnStunned;
            _health.OnHealthDecreased += HandleOnHealthDecreased;
            _stateMachine.ChangeState(_initialState);
        }
        private void Update()
        {
            _stateMachine.Update();
            if(LastHeardSound!= null)  //LastHeardProcesses in stateMachine Updates
            {
                LastHeardSound = null;
            }
            Debug.Log(_stateMachine.CurrentState);
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
        private void HandleOnStunned()
        {
            _stateMachine.ChangeState(AiStateId.Stunned);
        }
        private void HandleOnHealthDecreased()
        {
            _navMeshAgent.isStopped = true;
            StartCoroutine(ContinueNavMesh());
        }
        private IEnumerator ContinueNavMesh()
        {
            yield return new WaitForSeconds(1);
            _navMeshAgent.isStopped = false;
        }
    }

}
