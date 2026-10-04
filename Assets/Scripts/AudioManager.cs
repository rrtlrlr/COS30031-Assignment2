using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource buttonAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonAudio(string sceneName)
    {
        StartCoroutine(PlayThenLoadScene(sceneName));
    }

    private IEnumerator PlayThenLoadScene(string SceneName)
    {
        buttonAudio.PlayOneShot(buttonAudio.clip);
        yield return new WaitForSeconds(buttonAudio.clip.length);
        SceneManager.LoadScene(SceneName);
    }
}
