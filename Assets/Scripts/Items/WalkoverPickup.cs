using UnityEngine;

public class WalkoverPickup : MonoBehaviour
{
    public enum PickupType
    {
        Health,
        Ammo
    }

    [Header("Pickup Settings")]
    [SerializeField] private PickupType pickupType;
    [SerializeField] private int pickupAmount = 25;

    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (collected)
        {
            return;
        }

        Health health = other.GetComponentInParent<Health>();
        Ammo ammo = other.GetComponentInParent<Ammo>();

        if (pickupType == PickupType.Health)
        {
            if (health == null || health.Current >= health.Maximum)
            {
                return;
            }

            health.Heal(pickupAmount);
        }
        else if (pickupType == PickupType.Ammo)
        {
            if (ammo == null)
            {
                return;
            }

            ammo.AddReserveAmmo(pickupAmount);
        }

        collected = true;
        Destroy(gameObject);
    }
}