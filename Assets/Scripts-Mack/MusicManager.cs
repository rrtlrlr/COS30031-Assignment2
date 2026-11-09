using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource music;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        music = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level 1")
        {
            music.volume = 0.0f;
        }
        else if (scene.name == "MainMenu")
        {
            music.volume = 1.0f;
        }
        else if (scene.name == "LevelSelect")
        {
            music.volume = 1.0f;
        }
        else if (scene.name == "Level 2")
        {
            music.volume = 0.0f;
        }
        else if (scene.name == "Level 3")
        {
            music.volume = 0.0f;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
