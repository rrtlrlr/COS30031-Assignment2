using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Health playerHealth;
    [SerializeField] private Animator animator;

    [Header("Detection")]
    [SerializeField] private float detectionRange = 15f;
    [SerializeField] private float attackRange = 1.5f;

    [Header("Movement")]
    [SerializeField] private float movementSpeed = 2.5f;

    [Header("Attack")]
    [SerializeField] private int attackDamage = 15;
    [SerializeField] private float attackCooldown = 1.5f;

    private float nextAttackTime;
    private bool isDead;
    private bool isAttacking;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int AttackHash = Animator.StringToHash("IsAttacking");
    private static readonly int DeadHash = Animator.StringToHash("IsDead");

    private void Awake()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    private void Start()
    {
        if (agent != null)
        {
            agent.speed = movementSpeed;
        }
    }

    private void Update()
    {
        if (isDead || agent == null || !agent.enabled)
        {
            return;
        }

        if (player == null)
        {
            UpdateAnimation();
            return;
        }

        if (playerHealth != null && playerHealth.Current <= 0)
        {
            StopMovement();
            UpdateAnimation();
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            AttackPlayer();
        }
        else if (distance <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            StopMovement();
        }

        UpdateAnimation();
    }

    private void ChasePlayer()
    {
        isAttacking = false;

        agent.isStopped = false;
        agent.speed = movementSpeed;
        agent.SetDestination(player.position);

        if (animator != null)
        {
            animator.SetBool(AttackHash, false);
        }
    }

    private void StopMovement()
    {
        isAttacking = false;

        agent.isStopped = true;
        agent.ResetPath();

        if (animator != null)
        {
            animator.SetBool(AttackHash, false);
        }
    }

    private void AttackPlayer()
    {
        agent.isStopped = true;
        agent.ResetPath();

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * 8f
            );
        }

        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;

            isAttacking = true;

            if (animator != null)
            {
                animator.SetBool(AttackHash, true);
            }

            if (playerHealth != null)
            {
                playerHealth.ApplyDamage(
                    new DamageInfo(
                        attackDamage,
                        "Zombie",
                        gameObject,
                        player.position
                    )
                );
            }
        }
    }

    private void UpdateAnimation()
    {
        if (animator == null)
        {
            return;
        }

        float speed = 0f;

        if (agent != null && agent.enabled && !agent.isStopped)
        {
            speed = agent.velocity.magnitude;
        }

        animator.SetFloat(SpeedHash, speed);
    }

    public void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        isAttacking = false;

        if (agent != null && agent.enabled)
        {
            agent.isStopped = true;
            agent.ResetPath();
            agent.enabled = false;
        }

        if (animator != null)
        {
            animator.SetBool(AttackHash, false);
            animator.SetFloat(SpeedHash, 0f);
            animator.SetBool(DeadHash, true);
        }
    }
}