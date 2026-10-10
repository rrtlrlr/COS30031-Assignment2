using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(TMP_Text))]
public class DisplayPopup : MonoBehaviour
{
    private static readonly WaitForSeconds _waitThreeSeconds = new(3f);

    private TMP_Text _popupUI;
    private Coroutine _activeRoutine;

    private void Awake()
    {
        _popupUI = GetComponent<TMP_Text>();
    }

    public void ShowPopup(string text)
    {
        _popupUI.text = text;

        if (_activeRoutine != null)
        {
            StopCoroutine(_activeRoutine);
        }

        _activeRoutine = StartCoroutine(DisplayAndFade());
    }

    private IEnumerator DisplayAndFade()
    {
        // stay visible for 3s
        _popupUI.gameObject.SetActive(true);
        Color color = _popupUI.color;
        color.a = 1f;
        _popupUI.color = color;
        yield return _waitThreeSeconds;

        // fade out for 2s
        float fadeTime = 2f;
        float elapsed = 0f;

        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;

            color.a = Mathf.Lerp(1f, 0f, elapsed / fadeTime);
            _popupUI.color = color;

            yield return null;
        }

        color.a = 0f;
        _popupUI.color = color;
        _activeRoutine = null;
    }
}