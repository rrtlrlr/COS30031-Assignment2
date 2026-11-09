using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CodePanel : MonoBehaviour
{
    public GameObject panel;
    public TMP_InputField inputField;
    public DisplayPopup popup;
    private Section2DoorOpen _currentDoor;

    public void Open(Section2DoorOpen door)
    {
        _currentDoor = door;
        inputField.text = "";
        panel.SetActive(true);
        inputField.ActivateInputField();
        SetControl(false);
    }

    public void Close()
    {
        panel.SetActive(false);
        _currentDoor = null;
        SetControl(true);
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

    public static bool IsOpen;
    private void SetControl(bool on)
    {
        IsOpen = !on;
        Cursor.lockState = on ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !on;
    }
}