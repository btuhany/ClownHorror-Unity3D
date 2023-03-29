using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] int _health = 3;
    [SerializeField] float _invincibiltyCooldown = 0.7f;
    [HideInInspector] public bool IsDead;

    bool _isInvincible;

    public void DecreaseHealth()
    {
        if (!_isInvincible)
        {
            _health--;
            if (_health <= 0)
            {
                IsDead = true;
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
