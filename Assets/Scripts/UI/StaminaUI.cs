using UnityEngine;

public class StaminaUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Stamina playerStamina;
    [SerializeField] private PlayerController3D playerController;
    [SerializeField] private Transform staminaBar;
    [SerializeField] private Renderer backgroundRenderer;
    [SerializeField] private Renderer fillRenderer;

    [Header("Visibility")]
    [SerializeField] private float hideDelay = 1.5f;
    [SerializeField] private float fadeDuration = 0.35f;

    private Vector3 initialScale;
    private Vector3 initialPosition;

    private Material backgroundMaterial;
    private Material fillMaterial;

    private float timeSinceSprintStopped;
    private float visibility = 1f;

    private void Awake()
    {
        initialScale = staminaBar.localScale;
        initialPosition = staminaBar.localPosition;

        backgroundMaterial = backgroundRenderer.material;
        fillMaterial = fillRenderer.material;

        SetAlpha(1f);
    }

    private void Update()
    {
        if (playerStamina == null ||
            playerController == null ||
            staminaBar == null)
        {
            return;
        }

        UpdateStaminaBar();
        UpdateVisibility();
    }

    private void UpdateStaminaBar()
    {
        float maximumStamina = playerStamina.MaxStamina;

        float percentage = maximumStamina > 0f
            ? Mathf.Clamp01(playerStamina.CurrentStamina / maximumStamina)
            : 0f;

        staminaBar.localScale = new Vector3(
            initialScale.x * percentage,
            initialScale.y,
            initialScale.z
        );

        staminaBar.localPosition = new Vector3(
            initialPosition.x -
            initialScale.x * (1f - percentage) * 0.5f,
            initialPosition.y,
            initialPosition.z
        );
    }

    private void UpdateVisibility()
    {
        if (playerController.IsSprinting)
        {
            timeSinceSprintStopped = 0f;
            visibility = 1f;
        }
        else
        {
            timeSinceSprintStopped += Time.deltaTime;

            if (timeSinceSprintStopped >= hideDelay)
            {
                visibility = fadeDuration > 0f
                    ? Mathf.MoveTowards(
                        visibility,
                        0f,
                        Time.deltaTime / fadeDuration
                    )
                    : 0f;
            }
        }

        SetAlpha(visibility);
    }

    private void SetAlpha(float alpha)
    {
        SetMaterialAlpha(backgroundMaterial, alpha);
        SetMaterialAlpha(fillMaterial, alpha);
    }

    private void SetMaterialAlpha(Material material, float alpha)
    {
        if (material == null)
        {
            return;
        }

        Color color = material.color;
        color.a = alpha;
        material.color = color;
    }

    private void OnDestroy()
    {
        if (backgroundMaterial != null)
        {
            Destroy(backgroundMaterial);
        }

        if (fillMaterial != null)
        {
            Destroy(fillMaterial);
        }
    }
}