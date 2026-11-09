using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private int startingReserveAmmo = 120;

    private int _currentMagazineAmmo;
    private int _reserveAmmo;

    public int CurrentMagazineAmmo => _currentMagazineAmmo;
    public int ReserveAmmo => _reserveAmmo;
    public int MagazineSize => magazineSize;

    private void Awake()
    {
        _currentMagazineAmmo = magazineSize;
        _reserveAmmo = startingReserveAmmo;
    }

    public bool HasAmmo()
    {
        return _currentMagazineAmmo > 0;
    }

    public void UseAmmo(int amount)
    {
        _currentMagazineAmmo = Mathf.Max(0, _currentMagazineAmmo - amount);
    }

    public void Reload()
    {
        if (_currentMagazineAmmo >= magazineSize || _reserveAmmo <= 0)
        {
            return;
        }

        int needed = magazineSize - _currentMagazineAmmo;
        int amountToReload = Mathf.Min(needed, _reserveAmmo);

        _currentMagazineAmmo += amountToReload;
        _reserveAmmo -= amountToReload;
    }

    public void AddReserveAmmo(int amount)
    {
        _reserveAmmo += amount;
    }
}