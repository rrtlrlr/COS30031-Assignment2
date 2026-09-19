// Assets/Scripts/Health.cs - attach to anything that can be damaged.
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    // Anyone who cares subscribes. This component announces what happened and
    // never decides what happens next, which is what lets the same file work
    // unchanged on a player, a crate and a wall.
    public event Action<GameObject> Died;

    private int _current;

    public int CurrentHealth => _current;
    public int MaxHealth => maxHealth;

    private void Awake() => _current = maxHealth;

    public void TakeDamage(int amount)
    {
        _current = Mathf.Max(0, _current - amount);
        if (_current == 0)
        {
            Died?.Invoke(gameObject);
        }
    }
}