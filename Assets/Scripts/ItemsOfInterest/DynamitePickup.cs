using UnityEngine;

public class DynamitePickup : MonoBehaviour
{
    [Header("Dynamite Data")]
    [SerializeField] private ItemOfInterestData dynamiteData;

    public bool Collect()
    {
        if (dynamiteData == null)
        {
            Debug.LogWarning("Dynamite data is missing on " + gameObject.name);
            return false;
        }

        if (NotebookCollection.Instance == null)
        {
            Debug.LogWarning("NotebookCollection could not be found.");
            return false;
        }

        return NotebookCollection.Instance.CollectDynamite(dynamiteData);
    }
}