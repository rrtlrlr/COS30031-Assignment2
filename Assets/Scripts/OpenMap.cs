using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpenMap : MonoBehaviour
{
    public Image mapCanvas;
    public float fadeSpeed = 1f;

    private Coroutine fadeCoroutine;

    private void Start()
    {
        SetAlpha(0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            StartFade(1f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
        Color color = mapCanvas.color;

        while (!Mathf.Approximately(color.a, targetAlpha))
        {
            color.a = Mathf.MoveTowards(
                color.a,
                targetAlpha,
                fadeSpeed * Time.deltaTime
            );

            mapCanvas.color = color;
            yield return null;
        }
    }

    private void SetAlpha(float alpha)
    {
        Color color = mapCanvas.color;
        color.a = alpha;
        mapCanvas.color = color;
    }
}