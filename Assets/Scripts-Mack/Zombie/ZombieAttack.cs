using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private Animator animator;

    private Transform _player;
    private float _nextAttackTime;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            _player = player.transform;
        }
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
        }
    }

    private void Attack()
    {
        animator.SetBool("IsAttacking", true);
        _nextAttackTime = Time.time + attackCooldown;

        if (_player.TryGetComponent(out IDamageable target))
        {
            target.ApplyDamage(new DamageInfo(
                damage,
                "zombie",
                gameObject,
                transform.position
            ));
        }

        Invoke(nameof(StopAttackAnimation), 0.5f);
    }

    private void StopAttackAnimation()
    {
        animator.SetBool("IsAttacking", false);
    }

    private void OnDied(GameObject deadObject)
    {
        CancelInvoke(nameof(StopAttackAnimation));
        animator.SetBool("IsAttacking", false);
        enabled = false;
    }
}