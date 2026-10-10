using UnityEngine;
using UnityEngine.InputSystem;

public class ContextUI : MonoBehaviour
{
    [SerializeField] private GameObject contextPanel;
    [SerializeField] private InputActionReference continueAction;

    private void Start()
    {
        contextPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnEnable()
    {
        continueAction.action.Enable();
    }

    private void OnDisable()
    {
        continueAction.action.Disable();
    }

    public void Continue()
    {
        contextPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}