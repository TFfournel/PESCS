using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding: MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;

    [SerializeField] private float _MaxAngularSpeed;
    [SerializeField] private float _MaxAccelerationSpeed;
    [SerializeField] private float _StoppingDistance;

    [Header("PathFinding")]
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Vector3 target;

    public bool active = true;

    public void SetTarget(Vector3 pTarget,bool pSetActive = true)
    {
        target = pTarget;
        if(pSetActive)
            SetActive(true);
    }

    public void SetActive(bool pActive)
    {
        active = pActive;
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetNavMeshAgent();
    }

    // Update is called once per frame
    private void Update()
    {
        if(active)
            AgentCompute();
    }

    private void AgentCompute()
    {
        agent.SetDestination(target);
    }

    private void SetNavMeshAgent()
    {
        agent.speed = speed;
        agent.angularSpeed = _MaxAngularSpeed;
        agent.acceleration = _MaxAccelerationSpeed;
        agent.stoppingDistance = _StoppingDistance;
    }
}