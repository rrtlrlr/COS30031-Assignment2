using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private ZombieAI zombieAI;

    private void Awake()
    {
        if (health == null)
        {
            health = GetComponent<Health>();
        }

        if (zombieAI == null)
        {
            zombieAI = GetComponent<ZombieAI>();
        }
    }

    private void OnEnable()
    {
        if (health != null)
        {
            health.Died += OnDied;
        }
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.Died -= OnDied;
        }
    }

    private void OnDied(GameObject deadObject)
    {
        if (zombieAI != null)
        {
            zombieAI.Die();
        }
    }
}