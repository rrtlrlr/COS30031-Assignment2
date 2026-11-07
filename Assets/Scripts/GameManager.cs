using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private float elapsedTime;
    public float Section1Time { get; private set; } = -1f;
    public float Section2Time { get; private set; } = -1f;
    public float Section3Time { get; private set; } = -1f;

    void Update() => elapsedTime += Time.deltaTime;

    public void EndSection1() { Section1Time = elapsedTime; elapsedTime = 0f; }
    public void EndSection2() { Section2Time = elapsedTime; elapsedTime = 0f; }
    public void EndSection3() { Section3Time = elapsedTime; elapsedTime = 0f; }

    public bool PlayerHasKey { get; set; } = false;
    public int PlayerDynamiteCount { get; set; } = 0;
}