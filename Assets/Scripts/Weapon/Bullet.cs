using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float speed = 50f;
    [SerializeField] private int damage = 25;
    [SerializeField] private string damageType = "Bullet";
    [SerializeField] private float lifetime = 3f;

    private GameObject _shooter;
    private Rigidbody _rb;
    private bool _hasHit;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Initialize(GameObject shooter, float bulletSpeed)
    {
        _shooter = shooter;
        speed = bulletSpeed;

        if (_rb != null)
        {
            _rb.linearVelocity = transform.forward * speed;
        }

        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasHit)
        {
            return;
        }

        if (_shooter != null &&
            (collision.gameObject == _shooter ||
             collision.transform.IsChildOf(_shooter.transform)))
        {
            return;
        }

        Debug.Log("Bullet hit: " + collision.gameObject.name);
        _hasHit = true;

        IDamageable target = collision.gameObject.GetComponentInParent<IDamageable>();

        if (target != null)
        {
            Vector3 hitPoint = collision.GetContact(0).point;

            DamageInfo damageInfo = new DamageInfo(
                damage,
                damageType,
                _shooter,
                hitPoint
            );

            target.ApplyDamage(damageInfo);
        }

        Destroy(gameObject);
    }
}