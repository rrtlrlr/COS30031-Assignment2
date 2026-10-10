using UnityEngine;

public class Section3DoorOpen : MonoBehaviour
{
    public CodePanel codePanel;
    public GameObject itemDisplayImage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            codePanel.Open(this);
            itemDisplayImage.SetActive(false); // disable so it doesn't overlap with the code panel button
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            codePanel.Close();
            itemDisplayImage.SetActive(true); // re enable
        }
    }

    public bool TryCode(string code)
    {
        Debug.Log("Trycode called with code: " + code);
        if (code == "9275")
        {
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }
}