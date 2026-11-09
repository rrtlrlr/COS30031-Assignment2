using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 14f;
    [SerializeField] private float lifetime = 2f;

    private Rigidbody2D _body;
    private float _despawnAt;
    private bool _retired;

    public System.Action<Bullet> ReturnToPool { get; set; }

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 at, Vector2 direction, GameObject owner)
    {
        transform.position = at;
        _body.linearVelocity = direction.normalized * speed;
        _despawnAt = Time.time + lifetime;
        _retired = false;
    }

    private void Update()
    {
        if (Time.time >= _despawnAt)
        {
            Retire();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Retire();
    }

    private void Retire()
    {
        if (_retired)
        {
            return;
        }

        _retired = true;
        ReturnToPool?.Invoke(this);
    }
}