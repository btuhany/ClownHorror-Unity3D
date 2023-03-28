using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{
    [SerializeField] LayerMask _layer;
    [SerializeField] float _radius;
    bool _isHit;
    public bool IsAttacking;
    private void Update()
    {
        if (!IsAttacking) return;
        _isHit = Physics.CheckSphere(transform.position, _radius, _layer);
        if(_isHit)
        {
            Debug.Log("Player hit");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
