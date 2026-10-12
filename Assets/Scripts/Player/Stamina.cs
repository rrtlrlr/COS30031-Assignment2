using UnityEngine;

public class Stamina : MonoBehaviour
{
    [Header("Stamina")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float drainPerSecond = 25f;
    [SerializeField] private float recoveryPerSecond = 20f;
    [SerializeField] private float recoveryDelay = 1.5f;

    private float _currentStamina;
    private float _recoveryTimer;

    public float CurrentStamina => _currentStamina;
    public float MaxStamina => maxStamina;
    public bool CanSprint => _currentStamina > 0f;

    private void Awake()
    {
        _currentStamina = maxStamina;
    }

    private void Update()
    {
        if (_currentStamina >= maxStamina)
        {
            _currentStamina = maxStamina;
            return;
        }

        if (_recoveryTimer > 0f)
        {
            _recoveryTimer -= Time.deltaTime;
            return;
        }

        Recover(recoveryPerSecond * Time.deltaTime);
    }

    public bool HasStamina()
    {
        return _currentStamina > 0f;
    }

    public void Drain(float amount)
    {
        if (amount <= 0f)
        {
            return;
        }

        _currentStamina = Mathf.Max(0f, _currentStamina - amount);
        _recoveryTimer = recoveryDelay;
    }

    public void Recover(float amount)
    {
        if (amount <= 0f)
        {
            return;
        }

        _currentStamina = Mathf.Min(maxStamina, _currentStamina + amount);
    }
}