using UnityEngine;

public class CollectKey : MonoBehaviour
{
    public DisplayPopup popupUI;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.PlayerHasKey = true;
            popupUI.ShowPopup("You collected a Key!");
            gameObject.SetActive(false);
        }
    }
}