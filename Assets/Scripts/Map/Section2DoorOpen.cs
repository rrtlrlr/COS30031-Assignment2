using UnityEngine;

public class Section2DoorOpen : MonoBehaviour
{
    public CodePanel codePanel;
    public GameObject itemDisplayImage;

    private CanvasGroup _itemGroup;

    private void Awake()
    {
        // hide image with CanvasGroup instead of SetActive to fix a bug regarding unable to re-enable image
        if (!itemDisplayImage.TryGetComponent(out _itemGroup))
            _itemGroup = itemDisplayImage.AddComponent<CanvasGroup>();
    }

    // also run when the door is switched off after the right code
    private void OnDisable()
    {
        SetItemHidden(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            codePanel.Open(this);
            SetItemHidden(true); // so it doesn't overlap with the code panel button
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            codePanel.Close();
            SetItemHidden(false);
        }
    }

    private void SetItemHidden(bool hidden)
    {
        if (_itemGroup == null) return; // not created yet, or destroyed during scene unload
        _itemGroup.alpha = hidden ? 0f : 1f;
        _itemGroup.blocksRaycasts = !hidden; // an invisible image shouldnt block clicks on the code panel
    }

    public bool TryCode(string code)
    {
        Debug.Log("Trycode called with code: " + code);
        if (code == "592")
        {
            transform.parent.gameObject.SetActive(false);
            return true;
        }
        return false;
    }
}