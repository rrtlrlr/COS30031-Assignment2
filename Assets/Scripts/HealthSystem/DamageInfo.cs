using UnityEngine;

public readonly struct DamageInfo
{
    public readonly int Amount;
    public readonly string Type;
    public readonly GameObject Source;
    public readonly Vector2 Point;

    public DamageInfo(int amount, string type, GameObject source, Vector2 point)
    {
        Amount = amount;
        Type = type;
        Source = source;
        Point = point;
    }
}

public interface IDamageable
{
    void ApplyDamage(DamageInfo info);
}