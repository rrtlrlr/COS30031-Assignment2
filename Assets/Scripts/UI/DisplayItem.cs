using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayItem : MonoBehaviour
{
    public Image imageCanvas;
    public Sprite itemSprite;
    public float fadeSpeed = 1f;
    private Coroutine fadeCoroutine;

    private void Start() // initially invisible
    {
        SetAlpha(0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            imageCanvas.sprite = itemSprite;
            StartFade(1f); // fade to 1 (full opacity) on enter
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // dissipate to 0 (invisible) on exit
            StartFade(0f);
    }

    private void StartFade(float targetAlpha)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(Fade(targetAlpha));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        Color color = imageCanvas.color;

        while (!Mathf.Approximately(color.a, targetAlpha))
        {
            color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            imageCanvas.color = color;
            yield return null;
        }
    }

    private void SetAlpha(float alpha)
    {
        Color color = imageCanvas.color;
        color.a = alpha;
        imageCanvas.color = color;
    }
}