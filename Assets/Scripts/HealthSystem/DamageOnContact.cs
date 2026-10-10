// Assets/Scripts/DamageOnContact.cs - attach to a hazard or projectile that
// has a Collider2D with Is Trigger ticked.
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageOnContact : MonoBehaviour
{
    [SerializeField] private int amount = 10;
    [SerializeField] private string damageType = "physical";

    // Set by whatever fired this, so the shooter can be credited and cannot
    // damage itself. Left null on a static hazard, which is fine.
    public GameObject Owner { get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Note what is NOT here: no reference to Health, no tag check, no type
        // test. This works on anything that implements the contract, including
        // things written after this file.
        if (other.TryGetComponent(out IDamageable target))
        {
            target.ApplyDamage(
                new DamageInfo(amount, damageType, Owner, transform.position));
        }
    }
}