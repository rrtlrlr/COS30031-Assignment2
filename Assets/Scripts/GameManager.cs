/* GameManager.cs
 For global stuff like current game
 state, systems accessed by multiple
 scripts (e.g., score), pausing, etc.
 Accessed via GameManager.Instance
*/

using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject navMesh;
    private void Awake()
    {
        // ensure only one GameManager instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        navMesh.SetActive(true); // is disabled by default as it obstructs scene view during developent. Enable it when the scene first loads
    }
    public bool PlayerHasKey { get; set; } = false;
}