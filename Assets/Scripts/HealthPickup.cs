using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healthAmount = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.Heal(healthAmount);
            Destroy(gameObject);
        }
    }
}