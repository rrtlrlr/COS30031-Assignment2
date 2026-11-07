using UnityEngine;

public class ItemFloat : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 60f;

    [Header("Floating")]
    [SerializeField] private float bobbingHeight = 0.2f;
    [SerializeField] private float bobbingSpeed = 2f;

    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.localPosition;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        float newY = startingPosition.y +
                     Mathf.Sin(Time.time * bobbingSpeed) * bobbingHeight;

        transform.localPosition = new Vector3(
            startingPosition.x,
            newY,
            startingPosition.z
        );
    }
}