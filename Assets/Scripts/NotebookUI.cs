using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class NotebookUI : MonoBehaviour
{
    [SerializeField] private GameObject notebook;
    [SerializeField] private TMP_InputField noteInput;
    [SerializeField] private TMP_Text pageNumber;
    [SerializeField] private InputActionReference openNotebookAction;
    [SerializeField] private int pageCount = 5;

    private string[] _pages;
    private int _currentPage;

    private void Start()
    {
        _pages = new string[pageCount];
        _currentPage = 0;
        notebook.SetActive(false);
        UpdatePage();
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
        if (openNotebookAction.action.WasPressedThisFrame() && !notebook.activeSelf)
        {
            OpenNotebook();
        }
    }

    private void OpenNotebook()
    {
        notebook.SetActive(true);
        Time.timeScale = 0f;
        UpdatePage();
        noteInput.Select();
        noteInput.ActivateInputField();
    }

    public void CloseNotebook()
    {
        SaveCurrentPage();
        notebook.SetActive(false);
        Time.timeScale = 1f;
    }

    public void NextPage()
    {
        SaveCurrentPage();

        if (_currentPage < pageCount - 1)
        {
            _currentPage++;
            UpdatePage();
        }
    }

    public void PreviousPage()
    {
        SaveCurrentPage();

        if (_currentPage > 0)
        {
            _currentPage--;
            UpdatePage();
        }
    }

    private void SaveCurrentPage()
    {
        _pages[_currentPage] = noteInput.text;
    }

    private void UpdatePage()
    {
        noteInput.text = _pages[_currentPage];
        pageNumber.text = $"Page {_currentPage + 1} / {pageCount}";
    }
}