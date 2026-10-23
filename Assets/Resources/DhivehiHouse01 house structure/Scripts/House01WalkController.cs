using UnityEngine;

public sealed class House01WalkController : MonoBehaviour
{
    public CharacterController controller;
    public Camera playerCamera;
    public Light keyLight;
    public float moveSpeed = 3.4f;
    public float lookSensitivity = 2.0f;
#if ENABLE_LEGACY_INPUT_MANAGER
    private float pitch;

    private void Awake()
    {
        if (controller == null) controller = GetComponent<CharacterController>();
        if (playerCamera == null) playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera != null) pitch = playerCamera.transform.localEulerAngles.x;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetKeyDown(KeyCode.Escape)) Cursor.lockState = CursorLockMode.None;
        if (Cursor.lockState == CursorLockMode.Locked && playerCamera != null)
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * lookSensitivity, Space.World);
            pitch = Mathf.Clamp(pitch - Input.GetAxis("Mouse Y") * lookSensitivity, -78f, 78f);
            playerCamera.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        if (move.sqrMagnitude > 1f) move.Normalize();
        if (controller != null) controller.SimpleMove(transform.TransformDirection(move) * moveSpeed);
        if (keyLight != null && Input.GetKeyDown(KeyCode.Q)) keyLight.transform.RotateAround(Vector3.zero, Vector3.up, -25f);
        if (keyLight != null && Input.GetKeyDown(KeyCode.E)) keyLight.transform.RotateAround(Vector3.zero, Vector3.up, 25f);
    }
#endif

    private void OnGUI()
    {
#if ENABLE_LEGACY_INPUT_MANAGER
        GUI.Box(new Rect(16, 16, 520, 64), "Dhivehi House 01  |  WASD move  ·  click + mouse look  ·  Q/E rotate key light  ·  Esc release cursor");
#else
        GUI.Box(new Rect(16, 16, 520, 64), "Walk controls are disabled. Set Active Input Handling to Input Manager (Old) or Both to use this demo controller.");
#endif
    }
}
