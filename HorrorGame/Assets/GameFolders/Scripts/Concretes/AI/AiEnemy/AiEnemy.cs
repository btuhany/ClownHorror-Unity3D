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
        [SerializeField] AiEnemyDifficulties _initalDifficulty;
        [SerializeField] AiEnemyConfig _config;
        [SerializeField] EnemyCombatController _combat;
        AiEnemyDifficulties _currentDifficulty;
        AiStateMachine _stateMachine;
        NavMeshAgent _navMeshAgent;
        EnemyHealthController _health;
        Animator _anim;
        AiSoundController _soundController;
        [HideInInspector] public float[] CurrentMovementSpeeds;
        [HideInInspector] public float ChaseStateTimeout;
        private SightSensor _sightSensor;
        public Sound LastHeardSound;
        [HideInInspector] public Vector3 LastHeardSoundPos;


        public AiEnemyConfig Config { get => _config; }
        public Transform PlayerTransform { get => _playerTransform; }
        public NavMeshAgent NavMeshAgent { get => _navMeshAgent; }
        public AiStateMachine StateMachine { get => _stateMachine;  }
        public Animator Anim { get => _anim;  }
        public EnemyHealthController Health { get => _health;  }
        public EnemyCombatController Combat { get => _combat;  }
        public AiSoundController SoundController { get => _soundController; }

        private void Awake()
        {
           
            _currentDifficulty = _initalDifficulty;
            SetInitialMovementSpeed();
            _sightSensor = GetComponent<SightSensor>();
            _anim = GetComponent<Animator>();
            _health = GetComponent<EnemyHealthController>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _soundController = GetComponent<AiSoundController>();
            _stateMachine = new AiStateMachine(this);
            _stateMachine.RegisterState(new AiIdleState(this));
            _stateMachine.RegisterState(new AiChasePlayerState(this));
            _stateMachine.RegisterState(new AiWanderState(this));
            _stateMachine.RegisterState(new AiSeekPlayerState(this));
            _stateMachine.RegisterState(new AiGoToPointState(this));
            _stateMachine.RegisterState(new AiStunnedState(this));
            _stateMachine.RegisterState(new AiAttackState(this));
            _stateMachine.RegisterState(new AiCheckNoise(this));
            _stateMachine.RegisterState(new AiAgressiveChase(this));
        }
        private void OnEnable()
        {
            ChaseStateTimeout = _config.EasyChaseTimeout;
            ClownEventManager.Instance.OnEventStarted += HandleOnClownEvent;
            GameManager.Instance.OnNormalDiff += HandleOnNormalDiff;
            GameManager.Instance.OnHardDiff += HandleOnHardDiff;
            _health.OnStunned += HandleOnStunned;
            _health.OnHealthDecreased += HandleOnHealthDecreased;
           _stateMachine.ChangeState(_initialState);
        }
        private void SetInitialMovementSpeed()
        {
            if (_initalDifficulty == AiEnemyDifficulties.Easy)
            {
                CurrentMovementSpeeds = _config.EasyMovementSpeeds;
            }
            else if (_initalDifficulty == AiEnemyDifficulties.Normal)
            {
                CurrentMovementSpeeds = _config.NormalMovementSpeeds;
            }
            else if (_initalDifficulty == AiEnemyDifficulties.Hard)
            {
                CurrentMovementSpeeds = _config.HardMovementSpeeds;
            }
        }
        private void HandleOnHardDiff()
        {
            ChangeDifficulty(AiEnemyDifficulties.Hard);
            ChaseStateTimeout = _config.HardChaseTimeout;
        }

        private void HandleOnNormalDiff()
        {
            ChangeDifficulty(AiEnemyDifficulties.Normal);
            ChaseStateTimeout = _config.NormalChaseTimeout;
        }

        private void OnDisable()
        {
            _health.OnStunned -= HandleOnStunned;
            _health.OnHealthDecreased -= HandleOnHealthDecreased;
        }
        private void Update()
        {
            _stateMachine.Update();
            if(LastHeardSound!= null)  //LastHeardProcesses in stateMachine Updates
            {
                LastHeardSoundPos = LastHeardSound.Pos;
                LastHeardSound = null;
            }
            Debug.Log(_stateMachine.CurrentState);
        }
        public void ChangeDifficulty(AiEnemyDifficulties newDifficulty)
        {
            _currentDifficulty = newDifficulty;
            if (_currentDifficulty == AiEnemyDifficulties.Easy)
            {
                CurrentMovementSpeeds = _config.EasyMovementSpeeds;
            }
            else if (_currentDifficulty == AiEnemyDifficulties.Normal)
            {
                CurrentMovementSpeeds = _config.NormalMovementSpeeds;
            }
            else if (_currentDifficulty == AiEnemyDifficulties.Hard)
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
        public bool IsHeardSomething()
        {
            if(LastHeardSound != null)
            {
                if (!LastHeardSound.GameObject.CompareTag("Player"))
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
        private void HandleOnClownEvent()
        {
            _stateMachine.ChangeState(AiStateId.AggressiveChase);
         
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
            yield return new WaitForSeconds(_config.MaxStopTimeOnHealthDecrease);
            _navMeshAgent.isStopped = false;
        }
    }

}
