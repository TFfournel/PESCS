using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    public enum Team { Red, Blue }
    public Team CurrentTeam { get; set; }

    [Header("Movement")]
    [SerializeField] private float _MaxMovementSpeed;

    [SerializeField] private float _MaxAngularSpeed;
    [SerializeField] private float _MaxAccelerationSpeed;
    [SerializeField] private float _StoppingDistance;

    [Header("PathFinding")]
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform target;
    [SerializeField] private bool UseTarget = false;


    private void Start()
    {
        SetNavMeshAgent();
    }

    private void SetNavMeshAgent()
    {
        agent.speed = _MaxMovementSpeed * Random.Range(0.8f,1.2f);
        agent.angularSpeed = _MaxAngularSpeed;
        agent.acceleration = _MaxAccelerationSpeed;
        agent.stoppingDistance = _StoppingDistance;
    }

    private void Update()
    {
        PathFinding();
        DebugPathRay();
    }

    #region PathFinding
    private void PathFinding()
    {
        if(target != null && UseTarget)
        {
            agent.SetDestination(target.position);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    private void DebugPathRay()
    {
        Vector3[] lPath = agent.path.corners;
        if(lPath == null || lPath.Length < 2)
        {
            return;
        }

        for(int i = 0 ; i < lPath.Length - 1 ; i++)
        {
            Debug.DrawLine(lPath[i],lPath[i + 1],Color.green);
        }
    }

    public void SetTarget(Transform pTransform)
    {
        target = pTransform;
    }
    #endregion

    #region StateMachine

    #region ChooseWeapon
    private void SetChooseWeaponState()
    {

    }
    private void ChooseWeaponState()
    {

    }
    #endregion

    #region ChoosePoint
    private void SetChoosePointState()
    {

    }
    private void ChoosePointState()
    {

    }
    #endregion

    #region MoveToPoint
    private void SetMoveToPointState()
    {

    }
    private void MoveToPointState()
    {

    }
    #endregion

    #region Opposition
    private void SetOppositionState()
    {

    }
    private void OppositionState()
    {

    }
    #endregion

    #endregion
}