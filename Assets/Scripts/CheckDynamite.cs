using UnityEngine;

public class CheckDynamite : MonoBehaviour
{
    [SerializeField] private int requiredDynamite = 3;
    [SerializeField] private DisplayPopup popupUI;
    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (GameManager.Instance.PlayerDynamiteCount >= requiredDynamite)
        {
            popupUI.ShowPopup("You escaped the city! Proceed!");
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            popupUI.ShowPopup($"Not enough dynamite ({GameManager.Instance.PlayerDynamiteCount}/{requiredDynamite})");
        }
    }
}