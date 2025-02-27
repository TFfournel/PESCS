using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Bot : MonoBehaviour
{
    public enum Team { Red, Blue }
    [SerializeField] public Team CurrentTeam = Team.Red;
    [SerializeField] private BlueTeam _BlueTeam;
    [SerializeField] private RedTeam _RedTeam;

    [Header("Movement")]
    [SerializeField] private float _MaxMovementSpeed;

    [SerializeField] private float _MaxAngularSpeed;
    [SerializeField] private float _MaxAccelerationSpeed;
    [SerializeField] private float _StoppingDistance;

    [Header("PathFinding")]
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform target;
    [SerializeField] private bool UseTarget = false;

    [Header("Sites")]
    [SerializeField] private BoxCollider siteACollider;
    [SerializeField] private BoxCollider siteBCollider;
    public enum Site { SiteA, SiteB }
    [SerializeField] public Site CurrentSite = Site.SiteA;



    [Header("FieldOfView")]
    [SerializeField] public float fovAngle = 60;
    [SerializeField] public float fovRange = 2.6f;
    [SerializeField] public Vector2 lookDirection = Vector2.down;
    [SerializeField] private CapsuleCollider _DetectionColider;
    private List<Bot> _DsetectedBots = new List<Bot>();



    private Action _Action;

    private void Start()
    {
        SetChoosePointState();
        SetNavMeshAgent();
        SetDetectionZone();
    }

    private void SetNavMeshAgent()
    {
        agent.speed = _MaxMovementSpeed * Random.Range(0.8f,1.2f);
        agent.angularSpeed = _MaxAngularSpeed;
        agent.acceleration = _MaxAccelerationSpeed;
        agent.stoppingDistance = _StoppingDistance;
    }
    private void SetDetectionZone()
    {
        _DetectionColider.radius = fovRange;
    }

    private void Update()
    {
        _Action();
        //PathFinding();
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
        _Action = ChoosePointState;
    }
    private void ChoosePointState()
    {
        CurrentSite = (Site)Random.Range(0, 2);
        if (CurrentTeam == Team.Red) { CurrentSite = (Site)_RedTeam.GetSite(); }

        BoxCollider selectedCollider = (CurrentSite == Site.SiteA) ? siteACollider : siteBCollider;

        Vector3 randomPoint = GetRandomPointOnBoxCollider(selectedCollider);

        agent.SetDestination(randomPoint);

        SetMoveToPointState();
    }
    #endregion

    #region MoveToPoint
    private void SetMoveToPointState()
    {
        _Action = MoveToPointState;
    }

    private void MoveToPointState()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (CurrentTeam == Team.Blue) SetHoldState();
            else SetChoosePointState();
        }
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

    #region Hold
    private void SetHoldState()
    {
        _Action = HoldState;
    }
    private void HoldState()
    {
    }
    #endregion

    #endregion

    private Vector3 GetRandomPointOnBoxCollider(BoxCollider boxCollider)
    {
        Bounds bounds = boxCollider.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        Bot otherBot = other.GetComponent<Bot>();
        if (otherBot != null && otherBot.CurrentTeam != this.CurrentTeam)
        {
            if (!_DsetectedBots.Contains(otherBot))
            {
                _DsetectedBots.Add(otherBot);
                Debug.Log($"Detected enemy bot from team {otherBot.CurrentTeam}");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Bot otherBot = other.GetComponent<Bot>();
        if (otherBot != null && _DsetectedBots.Contains(otherBot))
        {
            _DsetectedBots.Remove(otherBot);
            Debug.Log($"Enemy bot from team {otherBot.CurrentTeam} left detection zone");
        }
    }

    public bool IsTargetInsideFOV(Transform target)
    {
        Vector2 directionToTarget = (target.position - transform.position).normalized;

        float angleToTarget = Vector2.Angle(lookDirection, directionToTarget);

        if (angleToTarget < fovAngle / 2)
        {
            float distance = Vector2.Distance(target.position, transform.position);

            return distance < fovRange;
        }

        return false;
    }

}