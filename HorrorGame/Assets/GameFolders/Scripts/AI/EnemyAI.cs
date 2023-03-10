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
    
    private void Awake()
    {
        _agentNavMesh= GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        

    }
    private void Start()
    {
        _animator.SetBool("IsRunning", true);
    }
    private void Update()
    {
        if(!_agentNavMesh.hasPath)
        {
            if (!_goToInstance)
                _agentNavMesh.SetDestination(GetPointForNavmesh.Instance.GetRandomPointFromTransform(transform, _radius));
            else
                _agentNavMesh.SetDestination(GetPointForNavmesh.Instance.GetRandomPointFromInstance());
        }
        if(_agentNavMesh.pathStatus)

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
#endif
}
