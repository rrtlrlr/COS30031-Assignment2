using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput2D))]
[RequireComponent(typeof(Stamina))]
public class PlayerSprint : MonoBehaviour
{
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float staminaDrainPerSecond = 25f;
    [SerializeField] private float staminaRecoveryPerSecond = 20f;
    [SerializeField] private InputActionReference sprintAction;

    private PlayerInput2D _movement;
    private Stamina _stamina;

    private void Awake()
    {
        _movement = GetComponent<PlayerInput2D>();
        _stamina = GetComponent<Stamina>();
    }

    private void OnEnable()
    {
        sprintAction.action.Enable();
    }

    private void OnDisable()
    {
        sprintAction.action.Disable();
    }

    private void Update()
    {
        bool sprinting = sprintAction.action.IsPressed() && _stamina.HasStamina();

        if (sprinting)
        {
            _movement.SetSpeedMultiplier(sprintMultiplier);
            _stamina.Drain(staminaDrainPerSecond * Time.deltaTime);
        }
        else
        {
            _movement.SetSpeedMultiplier(1f);
            _stamina.Recover(staminaRecoveryPerSecond * Time.deltaTime);
        }
    }
}