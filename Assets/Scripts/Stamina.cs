using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float drainPerSecond = 25f;
    [SerializeField] private float recoveryPerSecond = 20f;

    private float _currentStamina;

    public float CurrentStamina => _currentStamina;
    public float MaxStamina => maxStamina;

    private void Awake()
    {
        _currentStamina = maxStamina;
    }

    public bool HasStamina()
    {
        return _currentStamina > 0f;
    }

    public void Drain(float amount)
    {
        _currentStamina = Mathf.Max(0f, _currentStamina - amount);
    }

    public void Recover(float amount)
    {
        _currentStamina = Mathf.Min(maxStamina, _currentStamina + amount);
    }
}