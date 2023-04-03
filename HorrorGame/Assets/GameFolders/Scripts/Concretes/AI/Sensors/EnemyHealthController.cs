using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class EnemyHealthController : MonoBehaviour
    {
        [SerializeField] int _maxHealth = 12;
        [SerializeField] float _regenaratingTimer;
        float _regenerateCounter;
        int _currentHealth;
        bool _isStunned;

        public bool IsStunned { get => _isStunned; set => _isStunned = value; }

        public event System.Action OnHealthDecreased;
        public event System.Action OnStunned;
        private void Awake()
        {
            _currentHealth = _maxHealth;
            _regenerateCounter = _regenaratingTimer;
        }
        private void Update()
        {
            if (_currentHealth <= 0 && !_isStunned)
            {
                _isStunned = true;
                OnStunned?.Invoke();
                _currentHealth = _maxHealth;

            }
            else if (_currentHealth < _maxHealth)
            {
                RegenerateHealth();
            }

        }
        public void DecreaseHealth(int damage)
        {
            if (_isStunned) return;
            _currentHealth -= damage;
            if(_currentHealth > 0)
                OnHealthDecreased?.Invoke();
        }
        void RegenerateHealth()
        {
            if (_regenerateCounter < 0)
            {
                _regenerateCounter = _regenaratingTimer;
                _currentHealth++;
            }
            _regenerateCounter -= Time.deltaTime;
        }


    }

}
