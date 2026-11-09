using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Animator animator;
    [SerializeField] private float destroyDelay = 1f;

    private Rigidbody2D _body;
    private Collider2D _collider;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        health.Died += OnDied;
    }

    private void OnDisable()
    {
        health.Died -= OnDied;
    }

    private void OnDied(GameObject deadObject)
    {
        if (_body != null)
        {
            _body.linearVelocity = Vector2.zero;
            _body.angularVelocity = 0f;
            _body.bodyType = RigidbodyType2D.Kinematic;
        }

        if (_collider != null)
        {
            _collider.enabled = false;
        }

        if (animator != null)
        {
            animator.SetBool("IsDead", true);
        }

        Destroy(gameObject, destroyDelay);
    }
}