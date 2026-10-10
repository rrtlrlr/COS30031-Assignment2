using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Slider healthBar;

    private void Start()
    {
        healthBar.minValue = 0;
        healthBar.maxValue = playerHealth.Maximum;
        healthBar.value = playerHealth.Current;
    }

    private void Update()
    {
        healthBar.value = playerHealth.Current;
    }
}