using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using System.Collections;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Ammo ammo;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject shooter;
    [SerializeField] private Animator gunAnimator;

    [Header("Effects")]
    [SerializeField] private CinemachineImpulseSource impulseSource;
    [SerializeField] private LineRenderer tracerPrefab;

    [Header("Input")]
    [SerializeField] private InputActionReference fireAction;
    [SerializeField] private InputActionReference reloadAction;

    [Header("Shooting")]
    [SerializeField] private float fireRate = 10f;
    [SerializeField] private float maxAimDistance = 100f;
    [SerializeField] private LayerMask aimLayers = ~0;

    [Header("Tracer")]
    [SerializeField] private float tracerDuration = 0.06f;

    [Header("Reloading")]
    [SerializeField] private float reloadDuration = 3.63f;

    [Header("Weapon Recoil")]
    [SerializeField] private float recoilBack = 0.045f;
    [SerializeField] private float recoilPitch = 2f;
    [SerializeField] private float recoilRecoverySpeed = 12f;

    [Header("Muzzle Flash")]
    [SerializeField] private GameObject muzzleFlashObject;
    [SerializeField] private float muzzleFlashDuration = 0.08f;

    private float _nextFireTime;
    private bool _isReloading;

    private Coroutine _reloadCoroutine;
    private Coroutine _muzzleFlashCoroutine;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    private Vector3 _currentRecoilPosition;
    private Vector3 _targetRecoilPosition;

    private Vector3 _currentRecoilRotation;
    private Vector3 _targetRecoilRotation;

    public bool IsReloading => _isReloading;

    private void Awake()
    {
        _initialPosition = transform.localPosition;
        _initialRotation = transform.localRotation;

        if (muzzleFlashObject != null)
        {
            muzzleFlashObject.SetActive(false);
        }
    }

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

        if (_reloadCoroutine != null)
        {
            StopCoroutine(_reloadCoroutine);
            _reloadCoroutine = null;
        }

        if (_muzzleFlashCoroutine != null)
        {
            StopCoroutine(_muzzleFlashCoroutine);
            _muzzleFlashCoroutine = null;
        }

        if (muzzleFlashObject != null)
        {
            muzzleFlashObject.SetActive(false);
        }

        _isReloading = false;
    }

    private void Update()
    {
        if (fireAction != null && fireAction.action.IsPressed())
        {
            TryShoot();
        }

        if (reloadAction != null &&
            reloadAction.action.WasPressedThisFrame())
        {
            TryReload();
        }

        UpdateRecoil();
    }

    private void TryShoot()
    {
        if (_isReloading ||
            ammo == null ||
            bulletSpawnPoint == null ||
            playerCamera == null ||
            shooter == null)
        {
            return;
        }

        if (Time.time < _nextFireTime || !ammo.HasAmmo())
        {
            return;
        }

        if (!ammo.UseAmmo())
        {
            return;
        }

        _nextFireTime = Time.time + 1f / Mathf.Max(0.01f, fireRate);

        PlayShootingEffects();

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        Vector3 hitPoint = ray.GetPoint(maxAimDistance);

        RaycastHit[] hits = Physics.RaycastAll(
            ray,
            maxAimDistance,
            aimLayers,
            QueryTriggerInteraction.Ignore
        );

        System.Array.Sort(hits, (a, b) =>
            a.distance.CompareTo(b.distance));

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform == shooter.transform ||
                hit.transform.IsChildOf(shooter.transform))
            {
                continue;
            }

            hitPoint = hit.point;

            IDamageable target =
                hit.collider.GetComponentInParent<IDamageable>();

            if (target != null)
            {
                DamageInfo damageInfo = new DamageInfo(
                    25,
                    "Bullet",
                    shooter,
                    hit.point
                );

                target.ApplyDamage(damageInfo);
            }

            break;
        }

        CreateTracer(hitPoint);
    }

    private void PlayShootingEffects()
    {
        if (gunAnimator != null)
        {
            gunAnimator.SetTrigger("Shoot");
        }

        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }

        ApplyRecoil();
        PlayMuzzleFlash();
    }

    private void PlayMuzzleFlash()
    {
        if (muzzleFlashObject == null)
        {
            return;
        }

        if (_muzzleFlashCoroutine != null)
        {
            StopCoroutine(_muzzleFlashCoroutine);
        }

        muzzleFlashObject.SetActive(true);

        _muzzleFlashCoroutine = StartCoroutine(HideMuzzleFlash());
    }

    private IEnumerator HideMuzzleFlash()
    {
        yield return new WaitForSeconds(muzzleFlashDuration);

        if (muzzleFlashObject != null)
        {
            muzzleFlashObject.SetActive(false);
        }

        _muzzleFlashCoroutine = null;
    }

    private void CreateTracer(Vector3 hitPoint)
    {
        if (tracerPrefab == null)
        {
            return;
        }

        LineRenderer tracer = Instantiate(tracerPrefab);

        tracer.positionCount = 2;
        tracer.useWorldSpace = true;

        tracer.SetPosition(0, bulletSpawnPoint.position);
        tracer.SetPosition(1, hitPoint);

        Destroy(tracer.gameObject, tracerDuration);
    }

    private void ApplyRecoil()
    {
        _targetRecoilPosition += new Vector3(
            0f,
            0f,
            -recoilBack
        );

        _targetRecoilRotation += new Vector3(
            -recoilPitch,
            0f,
            0f
        );
    }

    private void UpdateRecoil()
    {
        _targetRecoilPosition = Vector3.Lerp(
            _targetRecoilPosition,
            Vector3.zero,
            recoilRecoverySpeed * Time.deltaTime
        );

        _targetRecoilRotation = Vector3.Lerp(
            _targetRecoilRotation,
            Vector3.zero,
            recoilRecoverySpeed * Time.deltaTime
        );

        _currentRecoilPosition = Vector3.Lerp(
            _currentRecoilPosition,
            _targetRecoilPosition,
            recoilRecoverySpeed * Time.deltaTime
        );

        _currentRecoilRotation = Vector3.Lerp(
            _currentRecoilRotation,
            _targetRecoilRotation,
            recoilRecoverySpeed * Time.deltaTime
        );

        transform.localPosition =
            _initialPosition + _currentRecoilPosition;

        transform.localRotation =
            _initialRotation *
            Quaternion.Euler(_currentRecoilRotation);
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

        _reloadCoroutine = StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        _isReloading = true;

        if (gunAnimator != null)
        {
            gunAnimator.SetTrigger("Reload");
        }

        yield return new WaitForSeconds(reloadDuration);

        if (ammo != null)
        {
            ammo.Reload();
        }

        _isReloading = false;
        _reloadCoroutine = null;
    }

}