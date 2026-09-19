// Assets/Scripts/PlayerInput2D.cs - attach to the Player GameObject.
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInput2D : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;

    // Drag Player/Move from InputSystem_Actions onto this field in the Inspector.
    [SerializeField] private InputActionReference moveAction;

    private Rigidbody2D _body;
    private Vector2 _input;

    private void Awake() => _body = GetComponent<Rigidbody2D>();

    private void OnEnable() => moveAction.action.Enable();

    private void OnDisable() => moveAction.action.Disable();

    private void Update()
    {
        // Read input per frame.
        _input = moveAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Move the physics body per physics step.
        _body.linearVelocity = _input.normalized * moveSpeed;
    }
}