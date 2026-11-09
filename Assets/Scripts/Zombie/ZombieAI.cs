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
    [SerializeField] private float movementSpeed = 3.5f;

    [Header("Attack")]
    [SerializeField] private int attackDamage = 15;
    [SerializeField] private float attackCooldown = 1.5f;

    private float nextAttackTime;
    private bool isDead;
    private bool isAttacking;

    private void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        if (agent != null)
            agent.speed = movementSpeed;
    }

    public void Initialize(Transform playerTransform, Health health)
    {
        player = playerTransform;
        playerHealth = health;
    }

    private void Update()
    {
        if (isDead || player == null || agent == null)
            return;

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
        agent.SetDestination(player.position);

        if (animator != null)
            animator.SetBool("IsAttacking", false);
    }

    private void StopMovement()
    {
        isAttacking = false;
        agent.isStopped = true;
        agent.ResetPath();

        if (animator != null)
            animator.SetBool("IsAttacking", false);
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
                animator.SetBool("IsAttacking", true);

            if (playerHealth != null)
            {
                playerHealth.ApplyDamage(
                    new DamageInfo(attackDamage, "Zombie", gameObject, player.position)
                );
            }
        }
    }

    private void UpdateAnimation()
    {
        if (animator == null || agent == null)
            return;

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void Die()
    {
        if (isDead)
            return;

        isDead = true;

        if (agent != null)
        {
            agent.isStopped = true;
            agent.ResetPath();
            agent.enabled = false;
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsDead", true);
        }
    }
}