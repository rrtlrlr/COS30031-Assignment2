using UnityEngine;
public class DisplayVictoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            victoryPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}