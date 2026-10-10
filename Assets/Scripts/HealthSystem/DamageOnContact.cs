using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageOnContact : MonoBehaviour
{
    [SerializeField] private int amount = 10;
    [SerializeField] private string damageType = "physical";

    public GameObject Owner { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable target = other.GetComponentInParent<IDamageable>();

        if (target != null)
        {
            target.ApplyDamage(
                new DamageInfo(amount, damageType, Owner, transform.position));
        }
    }
}