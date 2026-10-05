using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class OptionsMenu : MonoBehaviour
{
    [Header("UI References")]
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown; // Or use Dropdown if not using TMP

    private Resolution[] resolutions;

    void Start()
    {
        // --- Volume ---
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // --- Fullscreen ---
        bool savedFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.isOn = savedFullscreen;
        Screen.fullScreen = savedFullscreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // --- Resolution ---
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
    }

    public void CloseMenu()
    {
        PlayerPrefs.Save();
        gameObject.SetActive(false);
    }
}