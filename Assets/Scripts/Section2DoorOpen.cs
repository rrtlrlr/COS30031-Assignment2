using TMPro;
using UnityEngine;

public class Section2DoorOpen : MonoBehaviour
{
    public DisplayPopup popupUI;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.PlayerHasKey && collision.gameObject.CompareTag("Player"))
        {
            popupUI.ShowPopup("Section 2 unlocked!");
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            popupUI.ShowPopup("The door is locked. A key is required.");
        }
    }
}
