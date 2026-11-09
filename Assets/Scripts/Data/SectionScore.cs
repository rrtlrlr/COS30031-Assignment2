using UnityEngine;

[CreateAssetMenu(menuName = "Section Score")]
public class SectionScore : ScriptableObject
{
    public string levelName;
    public float bestTime = 60f; // finishing at this time = 5 stars

    // minimum percent needed for 1, 2, 3, 4, 5 stars
    public float[] starThresholds = { 20f, 40f, 60f, 80f, 95f };

    public float GetPercent(float timeTaken)
    {
        if (timeTaken <= 0f) return 100f;
        return Mathf.Clamp(bestTime / timeTaken * 100f, 0f, 100f);
    }

    public int GetStars(float timeTaken)
    {
        float percent = GetPercent(timeTaken);
        int stars = 0;
        for (int i = 0; i < starThresholds.Length; i++)
            if (percent >= starThresholds[i]) stars = i + 1;
        return stars;
    }

    public string GetStarMessage(float timeTaken)
    {
        int stars = GetStars(timeTaken);
        return $"You earned {stars} {(stars == 1 ? "star" : "stars")}.";
    }
}