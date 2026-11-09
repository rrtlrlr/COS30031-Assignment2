using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private InputActionReference pauseAction;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private bool _isPaused;

    public static bool IsPaused { get; private set; }

    private void Start()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
        IsPaused = false;
    }

    private void OnEnable()
    {
        if (pauseAction != null && pauseAction.action != null)
        {
            pauseAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (pauseAction != null && pauseAction.action != null)
        {
            pauseAction.action.Disable();
        }
    }

    private void Update()
    {
        if (pauseAction == null || pauseAction.action == null)
        {
            return;
        }

        if (!pauseAction.action.WasPressedThisFrame())
        {
            return;
        }

        if (NotebookUI.IsNotebookOpen)
        {
            return;
        }

        if (_isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (_isPaused || NotebookUI.IsNotebookOpen)
        {
            return;
        }

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
        IsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
        IsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        _isPaused = false;
        IsPaused = false;

        SceneManager.LoadScene(mainMenuSceneName);
    }
}