using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private ZombieAI zombieAI;
    [SerializeField] private Collider zombieCollider;
    [SerializeField] private ZombieDeath zombieDeath;

    private bool isDead;

    private void Awake()
    {
        if (health == null)
            health = GetComponent<Health>();

        if (zombieAI == null)
            zombieAI = GetComponent<ZombieAI>();

        if (zombieCollider == null)
            zombieCollider = GetComponent<CapsuleCollider>();

        if (zombieDeath == null)
            zombieDeath = GetComponent<ZombieDeath>();
    }

    private void OnEnable()
    {
        if (health != null)
            health.Died += OnDied;
    }

    private void OnDisable()
    {
        if (health != null)
            health.Died -= OnDied;
    }

    private void OnDied(GameObject deadObject)
    {
        if (isDead)
            return;

        isDead = true;

        if (zombieCollider != null)
            zombieCollider.enabled = false;

        if (zombieAI != null)
            zombieAI.Die();

        if (zombieDeath != null)
            zombieDeath.StartDeathSequence();
        else
            Destroy(gameObject, 4f);
    }
}