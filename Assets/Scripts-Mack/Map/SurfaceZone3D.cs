
// this reusable system goes on non trigger colliders that change how the player moves on it.
// PlayerController3D finds it by raycasting down thus needing no trigger or tag

using UnityEngine;
public class SurfaceZone3D : MonoBehaviour
{
    public SurfaceType Type = SurfaceType.Ice;
    public float IceAcceleration = 6f; // how fast you pick up speed / change direction
    public float IceDeceleration = 1.2f;  // how fast you slow down with no input (the slide)
    [Range(0.1f, 1f)] public float MudSpeedMultiplier = 0.5f; // how much slower you move on mud (0 to 1)
}
public enum SurfaceType { Ice, Mud }
