using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;

    public event Action<int, int> HealthChanged;
    public event Action<GameObject> Died;

    public int Current => _current;
    public int Maximum => maxHealth;

    private int _current;
    private bool _dead;

    private void Awake() => Revive();

    public void Revive()
    {
        _current = maxHealth;
        _dead = false;
        HealthChanged?.Invoke(_current, maxHealth);
    }

    public void ApplyDamage(DamageInfo info)
    {
        if (_dead || info.Source == gameObject)
        {
            return;
        }

        _current = Mathf.Max(0, _current - info.Amount);
        HealthChanged?.Invoke(_current, maxHealth);

        if (_current == 0)
        {
            _dead = true;
            Died?.Invoke(gameObject);
        }
    }
}