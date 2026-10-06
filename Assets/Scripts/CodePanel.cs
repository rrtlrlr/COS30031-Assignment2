using UnityEngine;
using TMPro;

public class CodePanel : MonoBehaviour
{
    public GameObject panel;
    public TMP_InputField inputField;
    public DisplayPopup popup;
    private Section3DoorOpen _currentDoor;

    public void Open(Section3DoorOpen door)
    {
        _currentDoor = door;
        inputField.text = "";
        panel.SetActive(true);
        inputField.ActivateInputField();
    }

    public void Close()
    {
        panel.SetActive(false);
        _currentDoor = null;
    }

    public void Submit()
    {
        Debug.Log("Submit called with input: " + inputField.text);
        if (_currentDoor == null) return;

        if (_currentDoor.TryCode(inputField.text))
        {
            Close();
            popup.ShowPopup("Section 3 unlocked!");
        }
        else
        {
            inputField.text = "";
            popup.ShowPopup("Wrong code. Have you been to the library?");
        }
    }
}