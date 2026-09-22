using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class NotebookUI : MonoBehaviour
{
    [SerializeField] private GameObject notebook;
    [SerializeField] private TMP_InputField noteInput;
    [SerializeField] private InputActionReference openNotebookAction;

    private void Start()
    {
        notebook.SetActive(false);
    }

    private void OnEnable()
    {
        openNotebookAction.action.Enable();
    }

    private void OnDisable()
    {
        openNotebookAction.action.Disable();
    }

    private void Update()
    {
        if (openNotebookAction.action.WasPressedThisFrame() && !noteInput.isFocused)
        {
            ToggleNotebook();
        }
    }

    private void ToggleNotebook()
    {
        bool isOpening = !notebook.activeSelf;

        notebook.SetActive(isOpening);
        Time.timeScale = isOpening ? 0f : 1f;
    }

    private void OpenNotebook()
    {
        notebook.SetActive(true);
        Time.timeScale = 0f;
        noteInput.Select();
        noteInput.ActivateInputField();
    }

    public void CloseNotebook()
    {
        notebook.SetActive(false);
        Time.timeScale = 1f;
    }
}