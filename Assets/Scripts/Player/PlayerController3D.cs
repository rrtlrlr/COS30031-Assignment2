using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController3D : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference sprintAction;

    [Header("Ground Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float groundAcceleration = 70f;
    [SerializeField] private float groundDeceleration = 45f;
    [SerializeField] private float groundFriction = 8f;

    [Header("Air Movement")]
    [SerializeField] private float airAcceleration = 18f;
    [SerializeField] private float airDeceleration = 0.5f;
    [SerializeField] private float airControl = 1f;
    [SerializeField] private float maxAirSpeed = 12f;

    [Header("Jumping")]
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -20f;

    private readonly RaycastHit[] _groundHits = new RaycastHit[8];

    private CharacterController _controller;
    private Vector3 _horizontalVelocity;
    private float _verticalVelocity;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
        sprintAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
        sprintAction.action.Disable();
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        input = Vector2.ClampMagnitude(input, 1f);

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 direction = forward * input.y + right * input.x;
        direction = Vector3.ClampMagnitude(direction, 1f);

        bool grounded = _controller.isGrounded;

        float targetSpeed = sprintAction.action.IsPressed()
            ? sprintSpeed
            : moveSpeed;

        SurfaceZone3D zone = grounded ? GetSurfaceUnderfoot() : null;

        if (zone != null && zone.Type == SurfaceType.Mud)
        {
            targetSpeed *= zone.MudSpeedMultiplier;
        }

        if (grounded)
        {
            HandleGroundMovement(direction, targetSpeed, input, zone);
        }
        else
        {
            HandleAirMovement(direction, targetSpeed, input);
        }

        if (grounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = -2f;
        }

        if (grounded && jumpAction.action.WasPressedThisFrame())
        {
            _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        _verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = _horizontalVelocity;
        velocity.y = _verticalVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }

    // Looks straight down from the player for a SurfaceZone3D (ice / mud).
    // RaycastNonAlloc + picking the closest zone means it still works if the
    // ice/mud mesh sits flush with the normal floor collider.
    private SurfaceZone3D GetSurfaceUnderfoot()
    {
        Bounds b = _controller.bounds;
        float distance = b.extents.y + 0.5f;

        int count = Physics.RaycastNonAlloc(
            b.center,
            Vector3.down,
            _groundHits,
            distance,
            ~0,
            QueryTriggerInteraction.Ignore
        );

        SurfaceZone3D best = null;
        float bestDistance = float.MaxValue;

        for (int i = 0; i < count; i++)
        {
            if (_groundHits[i].distance >= bestDistance) continue;

            SurfaceZone3D zone = _groundHits[i].collider.GetComponentInParent<SurfaceZone3D>();
            if (zone == null) continue;

            best = zone;
            bestDistance = _groundHits[i].distance;
        }

        return best;
    }

    private void HandleGroundMovement(Vector3 direction, float targetSpeed, Vector2 input, SurfaceZone3D zone)
    {
        float acceleration = groundAcceleration;
        float deceleration = groundDeceleration + groundFriction;

        if (zone != null && zone.Type == SurfaceType.Ice)
        {
            acceleration = zone.IceAcceleration;
            deceleration = zone.IceDeceleration;
        }

        if (input.sqrMagnitude > 0.01f)
        {
            Vector3 targetVelocity = direction * targetSpeed;

            _horizontalVelocity = Vector3.MoveTowards(
                _horizontalVelocity,
                targetVelocity,
                acceleration * Time.deltaTime
            );
        }
        else
        {
            _horizontalVelocity = Vector3.MoveTowards(
                _horizontalVelocity,
                Vector3.zero,
                deceleration * Time.deltaTime
            );
        }
    }

    private void HandleAirMovement(Vector3 direction, float targetSpeed, Vector2 input)
    {
        if (input.sqrMagnitude > 0.01f)
        {
            Vector3 airDirection = direction * targetSpeed;

            _horizontalVelocity = Vector3.MoveTowards(
                _horizontalVelocity,
                airDirection,
                airAcceleration * airControl * Time.deltaTime
            );
        }
        else
        {
            _horizontalVelocity = Vector3.MoveTowards(
                _horizontalVelocity,
                Vector3.zero,
                airDeceleration * Time.deltaTime
            );
        }

        _horizontalVelocity = Vector3.ClampMagnitude(
            _horizontalVelocity,
            maxAirSpeed
        );
    }
}