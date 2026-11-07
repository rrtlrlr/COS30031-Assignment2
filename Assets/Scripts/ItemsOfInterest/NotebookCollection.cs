using System;
using System.Collections.Generic;
using UnityEngine;

public class NotebookCollection : MonoBehaviour
{
    public static NotebookCollection Instance { get; private set; }

    private readonly List<ItemOfInterestData> collectedItems = new List<ItemOfInterestData>();
    private readonly HashSet<string> collectedItemIDs = new HashSet<string>();

    public event Action<ItemOfInterestData> ItemCollected;

    public IReadOnlyList<ItemOfInterestData> CollectedItems => collectedItems;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public bool CollectItem(ItemOfInterestData item)
    {
        if (item == null || string.IsNullOrWhiteSpace(item.ItemID))
        {
            return false;
        }

        if (!collectedItemIDs.Add(item.ItemID))
        {
            return false;
        }

        collectedItems.Add(item);
        ItemCollected?.Invoke(item);

        return true;
    }

    public bool HasCollected(string itemID)
    {
        return collectedItemIDs.Contains(itemID);
    }

    public List<ItemOfInterestData> GetItemsByCategory(ItemOfInterestData.Category category)
    {
        return collectedItems.FindAll(item => item.ItemCategory == category);
    }
}