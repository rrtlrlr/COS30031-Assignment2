using UnityEngine;

public class ZombieFaceMovement : MonoBehaviour
{
    [SerializeField] private Transform visual;

    private Vector3 _lastPosition;

    private void Start()
    {
        _lastPosition = transform.position;
    }

    private void Update()
    {
        Vector3 movement = transform.position - _lastPosition;

        if (movement.sqrMagnitude > 0.0001f)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            visual.rotation = Quaternion.Euler(0f, 0f, angle - 270f);
        }

        _lastPosition = transform.position;
    }
}