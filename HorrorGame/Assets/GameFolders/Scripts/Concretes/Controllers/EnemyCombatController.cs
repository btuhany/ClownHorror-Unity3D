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
    bool _isHitStarted;
    private void Update()
    {
        if (!IsAttacking) return; //can not be necesseray, created before event system
        if (!_isHitStarted) return;
        _isHit = Physics.CheckSphere(_attackHitSphere.position, _radius, _layer); //player layer
        if (_isHit)
        {
            _playerHealth.DecreaseHealth();
        }
    }
    public void StartHit() //trigger on animation event.
    {
        _isHitStarted = true;
    }
    public void FinishHit() //trigger on animation event.
    {
        _isHitStarted = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_attackHitSphere.position, _radius);
    }

}
