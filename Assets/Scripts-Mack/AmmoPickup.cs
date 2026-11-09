using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 30;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Ammo ammo))
        {
            ammo.AddReserveAmmo(ammoAmount);
            Destroy(gameObject);
        }
    }
}