using UnityEngine;
public class KeyPickup : MonoBehaviour
{
    public DisplayPopup popupUI;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.PlayerHasKey = true;
            popupUI.ShowPopup("You collected a Key!");
            gameObject.SetActive(false);
        }
    }
}