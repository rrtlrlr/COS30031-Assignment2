using UnityEngine;

public class CheckDynamite : MonoBehaviour
{
    public SectionScore section3Config;
    [SerializeField] private int requiredDynamite = 3;
    [SerializeField] private DisplayPopup popupUI;
    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (NotebookCollection.Instance.DynamiteCount >= requiredDynamite)
        {
            GameManager.Instance.EndSection3();
            float time = GameManager.Instance.Section3Time;
            popupUI.ShowPopup($"Proceed to Escape! Took {time:F2}s. {section3Config.GetStarMessage(time)}");
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            popupUI.ShowPopup($"Not enough dynamite ({NotebookCollection.Instance.DynamiteCount}/{requiredDynamite})");
        }
    }
}