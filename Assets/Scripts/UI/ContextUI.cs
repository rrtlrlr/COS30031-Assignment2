using UnityEngine;
using UnityEngine.InputSystem;

public class ContextUI : MonoBehaviour
{
    [SerializeField] private GameObject contextPanel;
    [SerializeField] private InputActionReference continueAction;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        contextPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnEnable()
    {
        continueAction.action.Enable();
    }

    private void OnDisable()
    {
        continueAction.action.Disable();
    }

    public void Continue()
    {
        contextPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}