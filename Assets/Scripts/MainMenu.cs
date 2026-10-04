using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] public Button PlayButton;
    [SerializeField] public Button ReturnButton;

    private AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame()
    {
        Debug.Log("Game Successfully Loaded");
        audioManager.PlayButtonAudio("LevelSelect");
    }
    
    public void ReturnToMenu()
    {
        Debug.Log("Main Menu Loaded Successfully");
        audioManager.PlayButtonAudio("MainMenu");
    }

    public void SelectLevelOne()
    {
        Debug.Log("Level 1 Successfully Loaded");
        audioManager.PlayButtonAudio("Level 1");
    }

    public void SelectLevelTwo()
    {
        Debug.Log("Level 2 Successfully Loaded");
        audioManager.PlayButtonAudio("Level 2");
    }

    public void SelectLevelThree()
    {
        Debug.Log("Level 3 Successfully Loaded");
        audioManager.PlayButtonAudio("Level 3");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
