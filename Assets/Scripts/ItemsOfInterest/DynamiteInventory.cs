using System;
using UnityEngine;

public class DynamiteInventory : MonoBehaviour
{
    public static DynamiteInventory Instance { get; private set; }

    [SerializeField] private int startingDynamite = 0;

    public int CurrentDynamite { get; private set; }

    public event Action<int> DynamiteChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        CurrentDynamite = startingDynamite;
    }

    public void AddDynamite(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        CurrentDynamite += amount;
        DynamiteChanged?.Invoke(CurrentDynamite);

        Debug.Log("Dynamite collected. Current amount: " + CurrentDynamite);
    }

    public bool UseDynamite(int amount)
    {
        if (amount <= 0 || CurrentDynamite < amount)
        {
            return false;
        }

        CurrentDynamite -= amount;
        DynamiteChanged?.Invoke(CurrentDynamite);

        return true;
    }
}