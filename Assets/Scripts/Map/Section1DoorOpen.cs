using UnityEngine;
public class Section1DoorOpen : MonoBehaviour
{
    public SectionScore section1Config;
    public DisplayPopup popupUI;
    void OnTriggerEnter(Collider collision)
    {
        if (GameManager.Instance.PlayerHasKey && collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.EndSection1();
            float time = GameManager.Instance.Section1Time;
            popupUI.ShowPopup($"Section 2 unlocked! Took {time:F2}s. {section1Config.GetStarMessage(time)}");
            transform.parent.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            popupUI.ShowPopup("The door is locked. A key is required.");
        }
    }
}