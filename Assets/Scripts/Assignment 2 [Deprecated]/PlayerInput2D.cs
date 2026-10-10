// Assets/Scripts/PlayerInput2D.cs - attach to the Player GameObject.
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInput2D : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float knockbackDecay = 25f;
    [SerializeField] private float bounceScale = 1.5f;
    [SerializeField] private float slideAccelPerFriction = 100f;

    private Vector2 _moveVelocity;
    private float _iceFriction = -1f; // -1 means not on ice

    private Rigidbody2D _body;
    private Vector2 _input;
    private Vector2 _knockback;
    private float _speedMultiplier = 1f;
    private float _surfaceMultiplier = 1f;
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
        // Fade the knockback toward zero.
        _knockback = Vector2.MoveTowards(_knockback, Vector2.zero, knockbackDecay * Time.fixedDeltaTime);

        // Input movement plus knockback.
        //_body.linearVelocity = _speedMultiplier * moveSpeed * _input.normalized + _knockback;

        // Added "surface multiplier" so certain surfaces (e.g., mud) can additionally affect speed.
        // also added ice friction logic for slippery surface
        Vector2 target = _speedMultiplier * _surfaceMultiplier * moveSpeed * _input.normalized;
        if (_iceFriction >= 0f)
        {
            // On ice: speed up and slow down gradually, so the player slides.
            float accel = _iceFriction * slideAccelPerFriction;
            _moveVelocity = Vector2.MoveTowards(_moveVelocity, target, accel * Time.fixedDeltaTime);
        }
        else
        {
            _moveVelocity = target;
        }

        _body.linearVelocity = _moveVelocity + _knockback;
    }
    public void SetIceFriction(float friction)
    {
        _iceFriction = friction;
    }
    public void SetSpeedMultiplier(float multiplier)
    {
        _speedMultiplier = multiplier;
    }
    public void SetSurfaceMultiplier(float multiplier)
    {
        _surfaceMultiplier = multiplier;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PhysicsMaterial2D mat = collision.collider.sharedMaterial;
        if (mat == null || mat.bounciness <= 0f) return;

        Vector2 normal = collision.GetContact(0).normal;
        float impactSpeed = collision.relativeVelocity.magnitude;

        // Bounce strength comes from the material's Bounciness.
        _knockback = bounceScale * impactSpeed * mat.bounciness * normal;
    }
}