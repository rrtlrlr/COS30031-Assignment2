using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject navMesh;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        navMesh.SetActive(true);
    }

    public bool PlayerHasKey { get; set; } = false;
    public int PlayerDynamiteCount { get; set; } = 0;
}