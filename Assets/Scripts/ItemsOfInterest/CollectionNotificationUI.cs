using TMPro;
using UnityEngine;
using System.Collections;

public class CollectionNotificationUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject notificationPanel;
    [SerializeField] private TMP_Text notificationTitle;
    [SerializeField] private TMP_Text notificationMessage;

    [Header("Settings")]
    [SerializeField] private float displayDuration = 3f;

    private Coroutine notificationCoroutine;

    private void Start()
    {
        if (notificationPanel != null)
        {
            notificationPanel.SetActive(false);
        }

        if (NotebookCollection.Instance != null)
        {
            NotebookCollection.Instance.ItemCollected += ShowNotification;
        }
        else
        {
            Debug.LogError("NotebookCollection instance was not found.");
        }
    }

    private void OnDestroy()
    {
        if (NotebookCollection.Instance != null)
        {
            NotebookCollection.Instance.ItemCollected -= ShowNotification;
        }
    }

    private void ShowNotification(ItemOfInterestData item)
    {
        if (item == null)
        {
            return;
        }

        ShowMessage(
            "ITEM DISCOVERED",
            item.ItemTitle + " has been added to your notebook."
        );
    }

    public void ShowMessage(string title, string message)
    {
        if (notificationPanel == null)
        {
            return;
        }

        if (notificationTitle != null)
        {
            notificationTitle.text = title;
        }

        if (notificationMessage != null)
        {
            notificationMessage.text = message;
        }

        notificationPanel.SetActive(true);

        if (notificationCoroutine != null)
        {
            StopCoroutine(notificationCoroutine);
        }

        notificationCoroutine = StartCoroutine(HideNotification());
    }

    private IEnumerator HideNotification()
    {
        yield return new WaitForSecondsRealtime(displayDuration);

        notificationPanel.SetActive(false);
        notificationCoroutine = null;
    }
}