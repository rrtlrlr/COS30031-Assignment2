using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private Stamina playerStamina;
    [SerializeField] private Slider staminaBar;

    private void Start()
    {
        staminaBar.minValue = 0;
        staminaBar.maxValue = playerStamina.MaxStamina;
        staminaBar.value = playerStamina.CurrentStamina;
    }

    private void Update()
    {
        staminaBar.value = playerStamina.CurrentStamina;
    }
}