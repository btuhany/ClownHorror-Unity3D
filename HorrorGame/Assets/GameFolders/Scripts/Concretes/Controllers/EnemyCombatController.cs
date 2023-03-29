using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{
    [SerializeField] Transform _attackHitSphere;
    [SerializeField] LayerMask _layer;
    [SerializeField] float _radius;
    [SerializeField] PlayerHealthController _playerHealth;
    bool _isHit;
    [HideInInspector] public bool IsAttacking;
    private void Update()
    {
        if (!IsAttacking) return;
        _isHit = Physics.CheckSphere(_attackHitSphere.position, _radius, _layer); //player layer
        if(_isHit)
        {
            _playerHealth.DecreaseHealth();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_attackHitSphere.position, _radius);
    }
}
