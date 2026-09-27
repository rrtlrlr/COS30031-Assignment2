using UnityEngine;

public class DisplayLocationName : MonoBehaviour
{
    public DisplayPopup popupUI;
    public string LocationName = "[unknown]";
    private bool firstTime = true;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (firstTime && collision.CompareTag("Player"))
        {
            popupUI.ShowPopup($"{LocationName} discovered!");
            firstTime = false;
        }
    }
}
