using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class NotebookUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject notebook;
    [SerializeField] private Transform notebookHolder;
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private TMP_InputField noteInput;
    [SerializeField] private TMP_Text pageNumber;
    [SerializeField] private InputActionReference openNotebookAction;
    [SerializeField] private MouseCursorController mouseCursorController;

    [Header("Notebook Tabs")]
    [SerializeField] private GameObject notesPanel;
    [SerializeField] private GameObject itemsPanel;
    [SerializeField] private TMP_Text titleText;

    [Header("Notebook Settings")]
    [SerializeField] private int pageCount = 5;
    [SerializeField] private float animationDuration = 0.4f;
    [SerializeField] private Vector3 hiddenPosition = new Vector3(0f, -1.5f, 0.7f);
    [SerializeField] private Vector3 visiblePosition = new Vector3(0f, -0.35f, 0.7f);

    private string[] _pages;
    private int _currentPage;
    private bool _isOpen;
    private bool _isAnimating;
    private bool _isOnNotesTab = true;
    private Coroutine _animationCoroutine;

    private void Start()
    {
        _pages = new string[pageCount];
        _currentPage = 0;

        notebookHolder.localPosition = hiddenPosition;
        notebook.SetActive(false);

        ShowNotesTab();
        UpdatePage();
    }

    private void OnEnable()
    {
        if (openNotebookAction != null)
        {
            openNotebookAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (openNotebookAction != null)
        {
            openNotebookAction.action.Disable();
        }
    }

    private void Update()
    {
        if (_isAnimating || openNotebookAction == null)
        {
            return;
        }

        if (_isOpen && _isOnNotesTab && noteInput != null && noteInput.isFocused)
        {
            if (Keyboard.current != null &&
                Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                CloseNotebook();
            }

            return;
        }

        if (openNotebookAction.action.WasPressedThisFrame())
        {
            if (_isOpen)
            {
                CloseNotebook();
            }
            else
            {
                OpenNotebook();
            }

            return;
        }

        if (_isOpen &&
            Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseNotebook();
        }
    }

    private void OpenNotebook()
    {
        if (_isOpen || _isAnimating)
        {
            return;
        }

        if (mouseCursorController != null)
        {
            mouseCursorController.enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        notebook.SetActive(true);

        if (weaponHolder != null)
        {
            weaponHolder.SetActive(false);
        }

        _animationCoroutine = StartCoroutine(
            AnimateNotebook(hiddenPosition, visiblePosition, true)
        );
    }

    public void CloseNotebook()
    {
        if (!_isOpen || _isAnimating)
        {
            return;
        }

        if (_isOnNotesTab)
        {
            SaveCurrentPage();
        }

        _animationCoroutine = StartCoroutine(
            AnimateNotebook(visiblePosition, hiddenPosition, false)
        );
    }

    private IEnumerator AnimateNotebook(Vector3 start, Vector3 end, bool opening)
    {
        _isAnimating = true;

        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.unscaledDeltaTime;

            float progress = Mathf.Clamp01(elapsed / animationDuration);
            float smoothProgress = Mathf.SmoothStep(0f, 1f, progress);

            notebookHolder.localPosition = Vector3.Lerp(
                start,
                end,
                smoothProgress
            );

            yield return null;
        }

        notebookHolder.localPosition = end;

        if (opening)
        {
            _isOpen = true;
            Time.timeScale = 0f;

            if (_isOnNotesTab)
            {
                UpdatePage();

                if (noteInput != null)
                {
                    noteInput.Select();
                    noteInput.ActivateInputField();
                }
            }
        }
        else
        {
            _isOpen = false;
            notebook.SetActive(false);

            if (weaponHolder != null)
            {
                weaponHolder.SetActive(true);
            }

            Time.timeScale = 1f;

            if (mouseCursorController != null)
            {
                mouseCursorController.enabled = true;
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        _isAnimating = false;
        _animationCoroutine = null;
    }

    public void ShowNotesTab()
    {
        if (notesPanel != null)
        {
            notesPanel.SetActive(true);
        }

        if (itemsPanel != null)
        {
            itemsPanel.SetActive(false);
        }

        if (titleText != null)
        {
            titleText.text = "My Notes";
        }

        _isOnNotesTab = true;

        if (_isOpen && noteInput != null)
        {
            UpdatePage();
            noteInput.Select();
            noteInput.ActivateInputField();
        }
    }

    public void ShowItemsTab()
    {
        if (_isOnNotesTab && _isOpen)
        {
            SaveCurrentPage();
        }

        if (notesPanel != null)
        {
            notesPanel.SetActive(false);
        }

        if (itemsPanel != null)
        {
            itemsPanel.SetActive(true);
        }

        if (titleText != null)
        {
            titleText.text = "Items of Interest";
        }

        _isOnNotesTab = false;
    }

    public void NextPage()
    {
        if (!_isOpen || !_isOnNotesTab)
        {
            return;
        }

        SaveCurrentPage();

        if (_currentPage < pageCount - 1)
        {
            _currentPage++;
            UpdatePage();
        }
    }

    public void PreviousPage()
    {
        if (!_isOpen || !_isOnNotesTab)
        {
            return;
        }

        SaveCurrentPage();

        if (_currentPage > 0)
        {
            _currentPage--;
            UpdatePage();
        }
    }

    private void SaveCurrentPage()
    {
        if (noteInput != null && _pages != null)
        {
            _pages[_currentPage] = noteInput.text;
        }
    }

    private void UpdatePage()
    {
        if (_pages == null)
        {
            return;
        }

        if (noteInput != null)
        {
            noteInput.text = _pages[_currentPage];
        }

        if (pageNumber != null)
        {
            pageNumber.text = $"Page {_currentPage + 1} / {pageCount}";
        }
    }
}