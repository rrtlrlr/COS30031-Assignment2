using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private Ammo ammo;
    [SerializeField] private TMP_Text ammoText;

    private void Update()
    {
        ammoText.text = $"{ammo.CurrentMagazineAmmo} / {ammo.ReserveAmmo}";
    }
}