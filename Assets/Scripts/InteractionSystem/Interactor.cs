using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private Transform interactionSource;
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private LayerMask interactionLayers = ~0;

    [Header("Input")]
    [SerializeField] private InputActionReference interactAction;

    private IInteractable activeInteraction;

    private void OnEnable()
    {
        interactAction?.action.Enable();
    }

    private void OnDisable()
    {
        interactAction?.action.Disable();
        activeInteraction = null;
    }

    private void Update()
    {
        if (interactAction == null ||
            !interactAction.action.WasPressedThisFrame())
        {
            return;
        }

        if (activeInteraction != null)
        {
            activeInteraction.Interact(this);
            return;
        }

        TryInteract();
    }

    private void TryInteract()
    {
        Ray ray = new Ray(
            interactionSource.position,
            interactionSource.forward
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactionRange,
            interactionLayers,
            QueryTriggerInteraction.Ignore))
        {
            IInteractable interactable =
                hit.collider.GetComponentInParent<IInteractable>();

            interactable?.Interact(this);
        }
    }

    public void SetActiveInteraction(IInteractable interactable)
    {
        activeInteraction = interactable;
    }

    public void ClearActiveInteraction(IInteractable interactable)
    {
        if (activeInteraction == interactable)
        {
            activeInteraction = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (interactionSource == null)
        {
            return;
        }

        Gizmos.color = Color.green;

        Gizmos.DrawRay(
            interactionSource.position,
            interactionSource.forward * interactionRange
        );
    }
}