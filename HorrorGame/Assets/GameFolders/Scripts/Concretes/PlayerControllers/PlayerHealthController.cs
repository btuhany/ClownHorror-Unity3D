using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] int _maxHealth = 3;
    [SerializeField] float _invincibiltyCooldown = 0.7f;
    [SerializeField] float _maxRegenerateTime = 10f;
    [HideInInspector] public bool IsDead;
    int _currentHealth;
    float _regenerateCounter;
  
    public event System.Action OnHealthDecreased;
    private void Awake()
    {
        _regenerateCounter = _maxRegenerateTime;
        _currentHealth = _maxHealth;
    }
    bool _isInvincible;
    private void Update()
    {
        if(_currentHealth < _maxHealth)
        {
            RegenerateHealth();
        }

    }
    void RegenerateHealth()
    {
        _regenerateCounter -= Time.deltaTime;
        if (_regenerateCounter < 0)
        {
            _currentHealth++;
            _regenerateCounter = _maxRegenerateTime;
        }
    }
    public void DecreaseHealth()
    {
        if (!_isInvincible)
        {
            _currentHealth--;
            _regenerateCounter = _maxRegenerateTime;
            OnHealthDecreased?.Invoke();
            if (_currentHealth <= 0)
            {
                IsDead = true;
                GameManager.Instance.GameOver();
            }
            StartCoroutine(InvincibileCooldown(_invincibiltyCooldown));
        }

    }
    IEnumerator InvincibileCooldown(float delay)
    {
        _isInvincible = true;
        yield return new WaitForSeconds(delay);
        _isInvincible = false;
        yield return null;
    }
}
