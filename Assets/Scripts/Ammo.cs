using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int startingAmmo = 30;

    private int _currentAmmo;

    public int CurrentAmmo => _currentAmmo;

    private void Awake()
    {
        _currentAmmo = startingAmmo;
    }

    public bool HasAmmo()
    {
        return _currentAmmo > 0;
    }

    public void UseAmmo(int amount)
    {
        _currentAmmo = Mathf.Max(0, _currentAmmo - amount);
    }

    public void AddAmmo(int amount)
    {
        _currentAmmo += amount;
    }
}