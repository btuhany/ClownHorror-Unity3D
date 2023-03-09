using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform _player;
    NavMeshAgent _navMesh;
    Animator _animator;
    private void Awake()
    {
        _navMesh= GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

    }
    private void Start()
    {
        _animator.SetBool("IsRunning", true);
    }
    private void Update()
    {
        _navMesh.SetDestination(_player.position);
        
    }
}
