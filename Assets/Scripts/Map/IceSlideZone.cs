using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class IceSlideZone : MonoBehaviour
{
    private float friction;

    private void Awake()
    {
        // slipperiness comes from ice material's friction.
        friction = GetComponent<PolygonCollider2D>().sharedMaterial.friction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.GetComponentInParent<PlayerInput2D>().SetIceFriction(friction);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            other.GetComponentInParent<PlayerInput2D>().SetIceFriction(-1f);
    }
}