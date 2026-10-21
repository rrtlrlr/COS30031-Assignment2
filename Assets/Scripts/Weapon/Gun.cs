using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Ammo ammo;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject shooter;

    [Header("Input")]
    [SerializeField] private InputActionReference fireAction;
    [SerializeField] private InputActionReference reloadAction;

    [Header("Shooting")]
    [SerializeField] private float fireRate = 10f;
    [SerializeField] private float bulletSpeed = 50f;
    [SerializeField] private float maxAimDistance = 100f;
    [SerializeField] private LayerMask aimLayers = ~0;

    [Header("Reloading")]
    [SerializeField] private float reloadDuration = 2f;

    private float _nextFireTime;
    private bool _isReloading;

    public bool IsReloading => _isReloading;

    private void OnEnable()
    {
        if (fireAction != null)
        {
            fireAction.action.Enable();
        }

        if (reloadAction != null)
        {
            reloadAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (fireAction != null)
        {
            fireAction.action.Disable();
        }

        if (reloadAction != null)
        {
            reloadAction.action.Disable();
        }
    }

    private void Update()
    {
        if (fireAction != null &&
            fireAction.action.IsPressed())
        {
            TryShoot();
        }

        if (reloadAction != null &&
            reloadAction.action.WasPressedThisFrame())
        {
            TryReload();
        }
    }

    private void TryShoot()
    {
        if (_isReloading ||
            ammo == null ||
            bulletPrefab == null ||
            bulletSpawnPoint == null ||
            playerCamera == null ||
            shooter == null)
        {
            return;
        }

        if (Time.time < _nextFireTime)
        {
            return;
        }

        if (!ammo.HasAmmo())
        {
            return;
        }

        if (!ammo.UseAmmo())
        {
            return;
        }

        _nextFireTime = Time.time + 1f / Mathf.Max(0.01f, fireRate);

        Vector3 aimPoint = GetAimPoint();
        Vector3 direction = (aimPoint - bulletSpawnPoint.position).normalized;

        if (direction.sqrMagnitude < 0.001f)
        {
            direction = playerCamera.transform.forward;
        }

        Bullet bullet = Instantiate(
            bulletPrefab,
            bulletSpawnPoint.position,
            Quaternion.LookRotation(direction)
        );

        bullet.Initialize(shooter, bulletSpeed);
    }

    private Vector3 GetAimPoint()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            maxAimDistance,
            aimLayers,
            QueryTriggerInteraction.Ignore))
        {
            if (hit.transform == shooter.transform ||
                hit.transform.IsChildOf(shooter.transform))
            {
                return ray.GetPoint(maxAimDistance);
            }

            return hit.point;
        }

        return ray.GetPoint(maxAimDistance);
    }

    private void TryReload()
    {
        if (_isReloading ||
            ammo == null ||
            ammo.IsMagazineFull ||
            !ammo.HasReserveAmmo)
        {
            return;
        }

        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        _isReloading = true;

        yield return new WaitForSeconds(reloadDuration);

        if (ammo != null)
        {
            ammo.Reload();
        }

        _isReloading = false;
    }
}