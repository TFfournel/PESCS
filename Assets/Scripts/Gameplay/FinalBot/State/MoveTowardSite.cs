using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardSite: Statee
{
    [Header("Navigation Settings")]
    [SerializeField] private bool useAllAreaMask = true; // Toggle to use all areas

    [SerializeField] private int specificAreaMask = NavMesh.AllAreas; // Specific area mask to use when not using all

    [Header("Position Settings")]
    public float gainingSpeed = .3f;

    public float searchDistance = 100f;
    public float maxAngle = 45f; // Maximum angle deviation from the forward direction
    public BoxCollider targetBoxCollider; // Reference to the BoxCollider

    public override void Initialize(StateManager lManager)
    {
        base.Initialize(lManager);
        SetMode();
    }

    public override void SetMode()
    {
        base.SetMode();

        // Get a random position within the BoxCollider
        Vector3 randomPos = GetRandomPointOnBoxCollider(targetBoxCollider);

        // Ensure the position is within the max distance and angle constraints
        randomPos = ConstrainPosition(randomPos,transform.position,searchDistance,maxAngle);

        // Decide which area mask to use
        int areaMask = useAllAreaMask ? NavMesh.AllAreas : specificAreaMask;

        // Get the closest valid NavMesh position
        randomPos = NavMeshExtensions.ClosestNavMeshPos(randomPos,searchDistance,areaMask).position;

        // Set the target position for pathfinding
        Pathfinding lPathfinding = stateManager.GetComponent<Pathfinding>();
        if(lPathfinding == null)
            lPathfinding = stateManager.AddComponent<Pathfinding>();
        lPathfinding.SetTarget(randomPos);
    }

    public override void DoAction()
    {
        base.DoAction();
    }

    public override float FactorCalculation()
    {
        base.FactorCalculation();
        return factor;
    }

    /// <summary>
    /// Gets a random point within the bounds of a BoxCollider.
    /// </summary>
    private Vector3 GetRandomPointOnBoxCollider(BoxCollider boxCollider)
    {
        if(boxCollider == null)
        {
            Debug.LogError("BoxCollider is not assigned!");
            return transform.position; // Fallback to current position
        }

        Bounds bounds = boxCollider.bounds;

        float randomX = Random.Range(bounds.min.x,bounds.max.x);
        float randomY = Random.Range(bounds.min.y,bounds.max.y);
        float randomZ = Random.Range(bounds.min.z,bounds.max.z);

        return new Vector3(randomX,randomY,randomZ);
    }

    /// <summary>
    /// Constrains the position to be within the max distance and angle from the origin.
    /// </summary>
    private Vector3 ConstrainPosition(Vector3 targetPos,Vector3 origin,float maxDistance,float maxAngle)
    {
        // Calculate the direction from the origin to the target position
        Vector3 direction = (targetPos - origin).normalized;

        // Clamp the distance
        float distance = Vector3.Distance(origin,targetPos);
        if(distance > maxDistance)
        {
            targetPos = origin + direction * maxDistance;
        }

        // Clamp the angle
        float angle = Vector3.Angle(transform.forward,direction);
        if(angle > maxAngle)
        {
            // Rotate the direction to stay within the max angle
            direction = Vector3.RotateTowards(transform.forward,direction,Mathf.Deg2Rad * maxAngle,0f);
            targetPos = origin + direction * distance;
        }

        return targetPos;
    }
}