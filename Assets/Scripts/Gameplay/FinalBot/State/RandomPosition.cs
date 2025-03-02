using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RandomPosition: StateFactorBaseIncrease
{
    [Header("Navigation Settings")]
    [SerializeField] private bool useAllAreaMask = true; // Toggle to use all areas

    [SerializeField] private int specificAreaMask = NavMesh.AllAreas; // Specific area mask to use when not using all
    public float gainingSpeed = .3f;
    public float searchDistance = 100f;

    public override void SetMode()
    {
        base.SetMode();

        Vector3 randomPos = RandomExtension.RandomPointInSphere(transform.position,searchDistance);

        // Decide which area mask to use
        int areaMask = useAllAreaMask ? NavMesh.AllAreas : specificAreaMask;

        // Get the closest valid NavMesh position
        randomPos = NavMeshExtensions.ClosestNavMeshPos(randomPos,searchDistance,areaMask).position;
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

    private Vector3 GetRandomPointOnBoxCollider(BoxCollider boxCollider)
    {
        Bounds bounds = boxCollider.bounds;

        float randomX = Random.Range(bounds.min.x,bounds.max.x);
        float randomY = Random.Range(bounds.min.y,bounds.max.y);
        float randomZ = Random.Range(bounds.min.z,bounds.max.z);

        return new Vector3(randomX,randomY,randomZ);
    }
}