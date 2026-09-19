using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private InputActionReference fireAction;
    [SerializeField] private float fireDistance = 0.5f;

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
    }

    private void OnDisable()
    {
        fireAction.action.Disable();
    }

    private void Update()
    {
        if (fireAction.action.WasPressedThisFrame() && _ammo.HasAmmo())
        {
            _ammo.UseAmmo(1);

            Vector2 direction = transform.right;
            Vector2 spawnPosition = (Vector2)transform.root.position + direction * fireDistance;

            Bullet bullet = _pool.Get();

            bullet.Launch(
                spawnPosition,
                direction,
                transform.root.gameObject);
        }
    }
}