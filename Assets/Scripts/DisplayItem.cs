using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class DisplayItem : MonoBehaviour
{
    public Image imageCanvas;
    public float fadeSpeed = 1f;

    private SpriteRenderer _spriteRenderer;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SetAlpha(0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            imageCanvas.sprite = _spriteRenderer.sprite;
            StartFade(1f);
        }
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
        Color color = imageCanvas.color;

        while (!Mathf.Approximately(color.a, targetAlpha))
        {
            color.a = Mathf.MoveTowards(
                color.a,
                targetAlpha,
                fadeSpeed * Time.deltaTime
            );

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