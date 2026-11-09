using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Transform healthBar;
    [SerializeField] private TMP_Text healthText;

    private Vector3 initialScale;
    private Vector3 initialPosition;

    private void Awake()
    {
        initialScale = healthBar.localScale;
        initialPosition = healthBar.localPosition;
    }

    private void Update()
    {
        if (playerHealth == null || healthBar == null || healthText == null)
        {
            return;
        }

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        int currentHealth = playerHealth.Current;
        int maximumHealth = playerHealth.Maximum;

        float healthPercentage = maximumHealth > 0
            ? Mathf.Clamp01((float)currentHealth / maximumHealth)
            : 0f;

        healthBar.localScale = new Vector3(
            initialScale.x * healthPercentage,
            initialScale.y,
            initialScale.z
        );

        healthBar.localPosition = new Vector3(
            initialPosition.x - (initialScale.x * (1f - healthPercentage) / 2f),
            initialPosition.y,
            initialPosition.z
        );

        healthText.text = currentHealth + " / " + maximumHealth;
    }
}