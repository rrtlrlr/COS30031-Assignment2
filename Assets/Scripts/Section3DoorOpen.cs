using UnityEngine;

public class Section3DoorOpen : MonoBehaviour
{
    public CodePanel codePanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            codePanel.Open(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            codePanel.Close();
    }

    public bool TryCode(string code)
    {
        if (code == "9275")
        {
            gameObject.SetActive(false);
            return true;
        }
        return false;
    }
}