using UnityEngine;
using UnityEngine.AI;

public class AiLocomotion : MonoBehaviour
{
    Animator _anim;
    NavMeshAgent _agent;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim= GetComponent<Animator>();
    }
    private void Update()
    {
        _anim.SetFloat("velocityZ", _agent.velocity.magnitude);
    }
}
