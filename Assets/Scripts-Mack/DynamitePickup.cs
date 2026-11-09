using UnityEngine;

public class DynamitePickup : MonoBehaviour
{
    public DisplayPopup popupUI;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.PlayerDynamiteCount += 1;
            popupUI.ShowPopup("You collected Dynamite! You now have " + GameManager.Instance.PlayerDynamiteCount);
            gameObject.SetActive(false);
        }
    }
}