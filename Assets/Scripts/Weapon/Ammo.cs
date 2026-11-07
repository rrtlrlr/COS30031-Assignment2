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
    public bool IsMagazineFull => _currentMagazineAmmo >= magazineSize;
    public bool HasReserveAmmo => _reserveAmmo > 0;

    private void Awake()
    {
        magazineSize = Mathf.Max(1, magazineSize);
        startingReserveAmmo = Mathf.Max(0, startingReserveAmmo);

        _currentMagazineAmmo = magazineSize;
        _reserveAmmo = startingReserveAmmo;
    }

    public bool HasAmmo()
    {
        return _currentMagazineAmmo > 0;
    }

    public bool UseAmmo(int amount = 1)
    {
        if (amount <= 0 || _currentMagazineAmmo < amount)
        {
            return false;
        }

        _currentMagazineAmmo -= amount;
        return true;
    }

    public bool Reload()
    {
        if (IsMagazineFull || !HasReserveAmmo)
        {
            return false;
        }

        int needed = magazineSize - _currentMagazineAmmo;
        int amountToReload = Mathf.Min(needed, _reserveAmmo);

        _currentMagazineAmmo += amountToReload;
        _reserveAmmo -= amountToReload;

        return amountToReload > 0;
    }

    public void AddReserveAmmo(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _reserveAmmo += amount;
    }
}