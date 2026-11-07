using UnityEngine;

public class DynamiteBlockade : MonoBehaviour, IInteractable
{
    [Header("Requirements")]
    [SerializeField] private int requiredDynamite = 3;

    [Header("Notification")]
    [SerializeField] private CollectionNotificationUI notificationUI;

    private bool isDestroyed;

    public void Interact(Interactor interactor)
    {
        TryDestroyBlockade();
    }

    private void TryDestroyBlockade()
    {
        if (isDestroyed)
        {
            return;
        }

        if (NotebookCollection.Instance == null)
        {
            Debug.LogWarning("NotebookCollection could not be found.");
            return;
        }

        if (!NotebookCollection.Instance.HasEnoughDynamite(requiredDynamite))
        {
            int currentAmount = NotebookCollection.Instance.DynamiteCount;
            int remaining = requiredDynamite - currentAmount;

            if (notificationUI != null)
            {
                notificationUI.ShowMessage(
                    "NOT ENOUGH DYNAMITE",
                    "You need " + remaining + " more dynamite to destroy this blockade."
                );
            }

            return;
        }

        if (!NotebookCollection.Instance.UseDynamite(requiredDynamite))
        {
            return;
        }

        isDestroyed = true;

        Destroy(gameObject);
    }
}