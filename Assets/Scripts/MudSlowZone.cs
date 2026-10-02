using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class MudSlowZone : MonoBehaviour
{
    private float slowRate;
    private void Awake()
    {
        // slowness comes from the mud material's friction.
        PolygonCollider2D mudCollider = GetComponent<PolygonCollider2D>();
        slowRate = 1f / (1f + mudCollider.sharedMaterial.friction);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SetSpeed(other, slowRate);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        SetSpeed(other, 1f);
    }

    void SetSpeed(Collider2D other, float multiplier)
    {
        if (other.CompareTag("Player"))
            other.GetComponentInParent<PlayerInput2D>().SetSurfaceMultiplier(multiplier);
        else if (other.CompareTag("Zombie"))
            other.GetComponentInParent<ZombieMovement>().SetSpeedMultiplier(multiplier);
    }
}