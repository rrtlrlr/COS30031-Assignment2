using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;

    [Header("UI References")]
    public GameObject pauseMenuPanel;
    public GameObject optionsPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsPanel != null && optionsPanel.activeSelf)
            {
                optionsPanel.SetActive(false);
                pauseMenuPanel.SetActive(true);
                return;
            }

            if (IsPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        pauseMenuPanel.transform.SetAsLastSibling(); // Force render on top
        Time.timeScale = 0f;
        AudioListener.pause = true;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        IsPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}