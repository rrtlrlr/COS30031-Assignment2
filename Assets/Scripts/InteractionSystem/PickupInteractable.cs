using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using System.Collections;

public class PickupInteractable : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private Transform holdPoint;
    [SerializeField] private Transform weaponVisualRoot;
    [SerializeField] private CinemachineInputAxisController cameraInput;
    [SerializeField] private GameObject crosshair;

    [Header("Input")]
    [SerializeField] private InputActionReference rotateAction;

    [Header("Rotation")]
    [SerializeField] private float rotationSensitivity = 0.2f;

    [Header("Weapon Animation")]
    [SerializeField] private float weaponLowerDistance = 0.8f;
    [SerializeField] private float weaponAnimationDuration = 0.3f;

    private Rigidbody rb;
    private Interactor currentInteractor;

    private Vector3 weaponOriginalPosition;
    private Vector3 weaponHiddenPosition;

    private Quaternion targetRotation;

    private bool isHeld;
    private bool isGamePaused;

    private float previousTimeScale;

    private Coroutine weaponAnimation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (weaponVisualRoot != null)
        {
            weaponOriginalPosition = weaponVisualRoot.localPosition;

            weaponHiddenPosition = weaponOriginalPosition +
                                   Vector3.down * weaponLowerDistance;
        }
    }

    private void OnEnable()
    {
        rotateAction?.action.Enable();
    }

    public void Interact(Interactor interactor)
    {
        if (!isHeld)
        {
            PickUpObject(interactor);
        }
        else
        {
            CollectObject();
        }
    }

    private void PickUpObject(Interactor interactor)
    {
        if (holdPoint == null)
        {
            return;
        }

        isHeld = true;
        currentInteractor = interactor;

        interactor.SetActiveInteraction(this);

        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        isGamePaused = true;

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        targetRotation = Quaternion.identity;

        if (cameraInput != null)
        {
            cameraInput.enabled = false;
        }

        if (crosshair != null)
        {
            crosshair.SetActive(false);
        }

        AnimateWeapon(weaponHiddenPosition);
    }

    private void Update()
    {
        if (isHeld)
        {
            HandleRotation();
        }
    }

    private void HandleRotation()
    {
        if (rotateAction == null ||
            !rotateAction.action.IsPressed() ||
            Mouse.current == null)
        {
            return;
        }

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        targetRotation *= Quaternion.Euler(
            -mouseDelta.y * rotationSensitivity,
            mouseDelta.x * rotationSensitivity,
            0f
        );

        transform.localRotation = targetRotation;
    }

    private void CollectObject()
    {
        ItemOfInterestPickup itemOfInterest = GetComponent<ItemOfInterestPickup>();

        if (itemOfInterest != null && !itemOfInterest.Collect())
        {
            return;
        }

        isHeld = false;

        if (currentInteractor != null)
        {
            currentInteractor.ClearActiveInteraction(this);
            currentInteractor = null;
        }

        if (cameraInput != null)
        {
            cameraInput.enabled = true;
        }

        if (crosshair != null)
        {
            crosshair.SetActive(true);
        }

        Time.timeScale = previousTimeScale;
        isGamePaused = false;

        if (weaponVisualRoot != null)
        {
            AnimateWeapon(weaponOriginalPosition, true);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void AnimateWeapon(Vector3 targetPosition, bool destroyAfter = false)
    {
        if (weaponAnimation != null)
        {
            StopCoroutine(weaponAnimation);
        }

        weaponAnimation = StartCoroutine(
            AnimateWeaponRoutine(targetPosition, destroyAfter)
        );
    }

    private IEnumerator AnimateWeaponRoutine(
        Vector3 targetPosition,
        bool destroyAfter)
    {
        Vector3 startPosition = weaponVisualRoot.localPosition;

        float elapsed = 0f;

        while (elapsed < weaponAnimationDuration)
        {
            elapsed += Time.unscaledDeltaTime;

            float progress = Mathf.Clamp01(
                elapsed / weaponAnimationDuration
            );

            float smoothProgress = Mathf.SmoothStep(0f, 1f, progress);

            weaponVisualRoot.localPosition = Vector3.Lerp(
                startPosition,
                targetPosition,
                smoothProgress
            );

            yield return null;
        }

        weaponVisualRoot.localPosition = targetPosition;
        weaponAnimation = null;

        if (destroyAfter)
        {
            Destroy(gameObject);
        }
    }
}