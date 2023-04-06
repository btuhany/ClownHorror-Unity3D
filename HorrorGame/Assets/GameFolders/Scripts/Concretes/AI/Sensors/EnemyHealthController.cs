using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using System;

namespace Controllers
{
    public class EnemyHealthController : MonoBehaviour
    {
        [SerializeField] int _maxHealth = 12;
        [SerializeField] float _regenaratingTimer;
        [SerializeField] EnemyHealthSliderController _slider;
        float _regenerateCounter;
        int _currentHealth;
        bool _isStunned;
        bool _isInEvent;
        AiSoundController _sound;
        public bool IsStunned { get => _isStunned; set => _isStunned = value; }

        public event System.Action OnHealthDecreased;
        public event System.Action OnStunned;
        private void Awake()
        {
            _sound = GetComponent<AiSoundController>();
            _currentHealth = _maxHealth;
            _regenerateCounter = _regenaratingTimer;
        }
        private void OnEnable()
        {
            ClownEventManager.Instance.OnEventStarted += HandleOnEventStarted;
            ClownEventManager.Instance.OnEventCompleted += HandleOnEventCompleted;
        }

        private void OnDisable()
        {
            ClownEventManager.Instance.OnEventStarted -= HandleOnEventStarted;
            ClownEventManager.Instance.OnEventCompleted -= HandleOnEventCompleted;
        }
        private void HandleOnEventCompleted()
        {
            _isInEvent = false;
        }

        private void HandleOnEventStarted()
        {
            _currentHealth = _maxHealth;
            _isInEvent = true;
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
                _slider.SetSlider(_currentHealth);
                if (_isInEvent) return;
                RegenerateHealth();
            }
            else if(_currentHealth == _maxHealth)
            {
                _slider.gameObject.SetActive(false);
            }

        }
        public void DecreaseHealth(int damage)
        {
            _sound.TakeHitSound();

            if (_isStunned) 
                return;

            if(damage>5)
            {
                _sound.LaughSound();
            }
                
            _currentHealth -= damage;
            _slider.gameObject.SetActive(true);
            
            if (_currentHealth > 0)
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
