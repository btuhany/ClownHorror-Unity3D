using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] float _radius;
    [SerializeField] bool _goToInstance;
    
    NavMeshAgent _agentNavMesh;
    Animator _animator;

    public float Radius { get => _radius; set => _radius = value; }

    private void Awake()
    {
        _agentNavMesh= GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        

    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (!_agentNavMesh.hasPath)  // changes automatically?! even it is not reached
        {
            if (!_goToInstance)
                _agentNavMesh.SetDestination(GetPointForNavmesh.Instance.GetRandomPointFromTransform(transform, _radius));
            else
                _agentNavMesh.SetDestination(GetPointForNavmesh.Instance.GetRandomPointFromInstance());
        }

    }


}
