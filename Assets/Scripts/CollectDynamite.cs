using UnityEngine;

public class CollectDynamite : MonoBehaviour
{
    public DisplayPopup popupUI;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.PlayerDynamiteCount += 1;
            popupUI.ShowPopup("You collected Dynamite! You now have " + GameManager.Instance.PlayerDynamiteCount);
            gameObject.SetActive(false);
        }
    }
}