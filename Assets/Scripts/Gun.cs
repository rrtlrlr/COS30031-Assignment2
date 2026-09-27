using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private InputActionReference fireAction;
    [SerializeField] private float fireDistance = 0.5f;
    [SerializeField] private InputActionReference reloadAction;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private Animator animator;

    private ObjectPool<Bullet> _pool;
    private Ammo _ammo;

    private void Awake()
    {
        _ammo = transform.root.GetComponent<Ammo>();

        _pool = new ObjectPool<Bullet>(
            createFunc: () =>
            {
                Bullet bullet = Instantiate(bulletPrefab);
                bullet.ReturnToPool = b => _pool.Release(b);
                return bullet;
            },
            actionOnGet: b => b.gameObject.SetActive(true),
            actionOnRelease: b => b.gameObject.SetActive(false),
            actionOnDestroy: b => Destroy(b.gameObject),
            collectionCheck: true,
            defaultCapacity: 20,
            maxSize: 100);
    }

    private void OnEnable()
    {
        fireAction.action.Enable();
        reloadAction.action.Enable();
    }

    private void OnDisable()
    {
        fireAction.action.Disable();
        reloadAction.action.Disable();
    }

    private float _nextFireTime;
    private bool _isReloading;

    private void Update()
    {
        bool isShooting = !_isReloading && fireAction.action.IsPressed() && _ammo.HasAmmo();

        animator.SetBool("IsShooting", isShooting);

        if (fireAction.action.WasPressedThisFrame() && !_isReloading && _ammo.HasAmmo())
        {
            animator.SetTrigger("Shoot");
        }

        if (reloadAction.action.WasPressedThisFrame() && !_isReloading)
        {
            StartReload();
        }

        if (!_isReloading && fireAction.action.IsPressed() && Time.time >= _nextFireTime && _ammo.HasAmmo())
        {
            _ammo.UseAmmo(1);

            Bullet bullet = _pool.Get();

            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            Vector2 direction = mouseWorldPosition - transform.root.position;
            direction.Normalize();

            Vector2 spawnPosition = (Vector2)transform.root.position + direction * fireDistance;

            bullet.Launch(
                spawnPosition,
                direction,
                transform.root.gameObject);

            _nextFireTime = Time.time + fireRate;
        }
    }

    private void StartReload()
    {
        if (_ammo.CurrentMagazineAmmo >= _ammo.MagazineSize || _ammo.ReserveAmmo <= 0)
        {
            return;
        }

        _isReloading = true;
        Invoke(nameof(FinishReload), reloadTime);
    }

    private void FinishReload()
    {
        _ammo.Reload();
        _isReloading = false;
    }
}