using UnityEngine;

[RequireComponent(typeof(Health))]
public class DespawnOnDeath : MonoBehaviour
{
    private Health _health;

    private void Awake() => _health = GetComponent<Health>();

    private void OnEnable() => _health.Died += HandleDied;

    private void OnDisable() => _health.Died -= HandleDied;

    private void HandleDied(GameObject who) => Destroy(who);
}