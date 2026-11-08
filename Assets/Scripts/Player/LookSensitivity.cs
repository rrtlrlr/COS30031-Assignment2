using Unity.Cinemachine;
using UnityEngine;
[RequireComponent(typeof(CinemachineInputAxisController))]
public class LookSensitivity : MonoBehaviour
{
    public const string PrefKey = "Sensitivity";
    public static LookSensitivity Instance { get; private set; }
    private CinemachineInputAxisController _controller;
    private float[] _baseGains; // gains in the inspector where the multiplier scales this

    private void Awake()
    {
        Instance = this;
        _controller = GetComponent<CinemachineInputAxisController>();

        _baseGains = new float[_controller.Controllers.Count];
        for (int i = 0; i < _baseGains.Length; i++)
            _baseGains[i] = _controller.Controllers[i].Input.Gain;

        // apply saved setting on game scene load, whether or not options was opened
        Apply(PlayerPrefs.GetFloat(PrefKey, 1f));
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public void Apply(float multiplier)
    {
        for (int i = 0; i < _baseGains.Length; i++)
            _controller.Controllers[i].Input.Gain = _baseGains[i] * multiplier;
    }
}
