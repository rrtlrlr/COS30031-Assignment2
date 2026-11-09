    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private InputActionReference pauseAction;
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    private bool _isPaused;
    private void Start()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    private void OnEnable()
    {
        pauseAction.action.Enable();
    }

    private void OnDisable()
    {
        pauseAction.action.Disable();
    }

    private void Update()
    {
        if (pauseAction.action.WasPressedThisFrame())
        {
            if (_isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public static bool IsPaused;

    public void Pause()
    {
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
        IsPaused = false;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}