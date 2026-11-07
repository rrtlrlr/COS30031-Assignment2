using UnityEngine;

public class ItemOfInterestPickup : MonoBehaviour
{
    [Header("Item Data")]
    [SerializeField] private ItemOfInterestData itemData;

    public bool Collect()
    {
        if (itemData == null)
        {
            Debug.LogWarning("No Item of Interest Data assigned to " + gameObject.name);
            return false;
        }

        if (NotebookCollection.Instance == null)
        {
            Debug.LogWarning("NotebookCollection could not be found in the scene.");
            return false;
        }

        return NotebookCollection.Instance.CollectItem(itemData);
    }
}