using AI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClownEventManager : SingletonMonoObject<ClownEventManager>
{
    [SerializeField] Transform _player;
    [SerializeField] Transform _enemyWhiteClown;
    [SerializeField] AiEnemy _enemy;
    [SerializeField] Transform[] _eventRandomEnemyPositions;
    [SerializeField] Transform _eventPlayerPos;

    public event System.Action OnEventStarted;
    public event System.Action OnEventCompleted;
    private bool _isInEvent;

    Vector3 _lastPlayerPos;
    Vector3 _lastEnemyPos;

    private void Awake()
    {
        SingletonThisObject(this);
    }
    private void Update()
    {
        if(_isInEvent && _enemy.StateMachine.CurrentState == AiStateId.Stunned)
        {  
            FinishEvent();
        }
    }
    private void FinishEvent()
    {
        _isInEvent = false;
        OnEventCompleted?.Invoke();
        MoveGameObjectsToLastPositions();
        

    }
    
    void MoveGameObjectsToEventArea()
    {
        _lastPlayerPos = _player.position;
        _lastEnemyPos = _enemyWhiteClown.position;
        _player.transform.position = _eventPlayerPos.position;
        _enemyWhiteClown.GetComponent<NavMeshAgent>().enabled = false;
        _enemyWhiteClown.transform.position = _eventRandomEnemyPositions[Random.Range(0, _eventRandomEnemyPositions.Length)].position;
        _enemyWhiteClown.GetComponent<NavMeshAgent>().enabled = true;
    }
    void MoveGameObjectsToLastPositions()
    {
        _player.transform.DOMove(_lastPlayerPos,0.1f);  //doesn't work otherwise?
        _enemyWhiteClown.GetComponent<NavMeshAgent>().enabled = false;
        _enemyWhiteClown.transform.position = _lastEnemyPos;
        _enemyWhiteClown.GetComponent<NavMeshAgent>().enabled = true;
    }
    public void StartEvent()
    {
        OnEventStarted?.Invoke();
        MoveGameObjectsToEventArea();
        _isInEvent = true;
    }

}
