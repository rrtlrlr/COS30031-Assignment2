using System;
using System.Collections.Generic;
using UnityEngine;

public class NotebookCollection : MonoBehaviour
{
    public static NotebookCollection Instance { get; private set; }

    private readonly List<ItemOfInterestData> collectedItems = new List<ItemOfInterestData>();
    private readonly HashSet<string> collectedItemIDs = new HashSet<string>();

    private int dynamiteCount;

    public event Action<ItemOfInterestData> ItemCollected;
    public event Action<int> DynamiteCountChanged;

    public IReadOnlyList<ItemOfInterestData> CollectedItems => collectedItems;
    public int DynamiteCount => dynamiteCount;

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

        if (item.ItemCategory == ItemOfInterestData.Category.Dynamite)
        {
            return CollectDynamite(item);
        }

        if (!collectedItemIDs.Add(item.ItemID))
        {
            return false;
        }

        collectedItems.Add(item);
        ItemCollected?.Invoke(item);

        return true;
    }

    public bool CollectDynamite(ItemOfInterestData item)
    {
        if (item == null || item.ItemCategory != ItemOfInterestData.Category.Dynamite)
        {
            return false;
        }

        bool firstPickup = collectedItemIDs.Add(item.ItemID);

        if (firstPickup)
        {
            collectedItems.Add(item);
            ItemCollected?.Invoke(item);
        }

        dynamiteCount++;
        DynamiteCountChanged?.Invoke(dynamiteCount);

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

    public bool HasEnoughDynamite(int requiredAmount)
    {
        return dynamiteCount >= requiredAmount;
    }

    public bool UseDynamite(int amount)
    {
        if (amount <= 0 || dynamiteCount < amount)
        {
            return false;
        }

        dynamiteCount -= amount;
        DynamiteCountChanged?.Invoke(dynamiteCount);

        return true;
    }
}