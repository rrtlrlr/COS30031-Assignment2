using System;
using Unity.VisualScripting;
using UnityEngine;

public class CheckDynamite : MonoBehaviour
{
    [SerializeField] private int requiredDynamite = 1;
    [SerializeField] private GameObject objectToDestroy;
    [SerializeField] private DisplayPopup popupUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        if (GameManager.Instance.PlayerDynamiteCount >= requiredDynamite)
        {
            GameManager.Instance.PlayerDynamiteCount -= requiredDynamite;
            popupUI.ShowPopup("You used " + requiredDynamite + " dynamite!");
            Destroy(objectToDestroy);
        }
        else
        {
            popupUI.ShowPopup("Not enough dynamite!");
        }
    }
}
