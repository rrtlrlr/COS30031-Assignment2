using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _player;
    private float _baseSpeed;
    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _baseSpeed = _agent.speed;
    }
    public void SetSpeedMultiplier(float multiplier)
    {
        _agent.speed = _baseSpeed * multiplier;
    }

    private void Update()
    {
        if (_player == null)
        {
            return;
        }

        _agent.SetDestination(_player.position);
    }
}