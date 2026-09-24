using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackCooldown = 1f;

    private Transform _player;
    private float _nextAttackTime;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (_player == null)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, _player.position);

        if (distance <= attackRange && Time.time >= _nextAttackTime)
        {
            Attack();
            _nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void Attack()
    {
        if (_player.TryGetComponent(out IDamageable target))
        {
            target.ApplyDamage(new DamageInfo(
                damage,
                "zombie",
                gameObject,
                transform.position));
        }
    }
}