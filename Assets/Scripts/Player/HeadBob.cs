using UnityEngine;
using UnityEngine.InputSystem;

public class HeadBob : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float walkFrequency = 1.8f;
    [SerializeField] private float sprintFrequency = 2.8f;
    [SerializeField] private float walkAmplitude = 0.035f;
    [SerializeField] private float sprintAmplitude = 0.06f;
    [SerializeField] private float returnSpeed = 8f;
    [SerializeField] private float movementThreshold = 0.1f;

    private Vector3 _initialLocalPosition;
    private float _bobTimer;

    private void Awake()
    {
        _initialLocalPosition = cameraTarget.localPosition;
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        Vector3 horizontalVelocity = characterController.velocity;
        horizontalVelocity.y = 0f;

        bool isMoving = horizontalVelocity.magnitude > movementThreshold;
        bool isGrounded = characterController.isGrounded;

        float targetOffset = 0f;

        if (isMoving && isGrounded)
        {
            bool isSprinting = Keyboard.current != null &&
                               Keyboard.current.leftShiftKey.isPressed;

            float frequency = isSprinting ? sprintFrequency : walkFrequency;
            float amplitude = isSprinting ? sprintAmplitude : walkAmplitude;

            _bobTimer += Time.deltaTime * frequency * Mathf.PI * 2f;
            targetOffset = Mathf.Sin(_bobTimer) * amplitude;
        }

        Vector3 targetPosition = _initialLocalPosition;
        targetPosition.y += targetOffset;

        float smoothing = isMoving && isGrounded ? 15f : returnSpeed;

        cameraTarget.localPosition = Vector3.Lerp(
            cameraTarget.localPosition,
            targetPosition,
            smoothing * Time.deltaTime
        );
    }
}