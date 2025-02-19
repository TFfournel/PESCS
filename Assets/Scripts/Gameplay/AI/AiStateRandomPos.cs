using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine;

using UnityEngine.AI;  // This is the required namespace for NavMesh

public class AiStateRandomPos: AiState
{
    [Header("Navigation Settings")]
    [SerializeField] private bool useAllAreaMask = true; // Toggle to use all areas

    [SerializeField] private int specificAreaMask = NavMesh.AllAreas; // Specific area mask to use when not using all
    public float gainingSpeed = .3f;
    public float searchDistance = 100f;

    protected override void Init(AiStateParameter pParam)
    {
        base.Init(pParam);
    }

    public override void Factor()
    {
        base.Factor();
        changeToStateFactor += gainingSpeed * Time.deltaTime;
    }

    protected override void SetChangeState()
    {
        base.SetChangeState();

        Vector3 randomPos = RandomExtension.RandomPointInSphere(transform.position,searchDistance);

        // Decide which area mask to use
        int areaMask = useAllAreaMask ? NavMesh.AllAreas : specificAreaMask;

        // Get the closest valid NavMesh position
        randomPos = NavMeshExtensions.ClosestNavMeshPos(randomPos,searchDistance,areaMask).position;

        aiStateManager.aiValues.pathfinding.SetTarget(randomPos);
        changeToStateFactor = 0;
        Debug.Log("set change state" + " random pos" + randomPos);
    }

    public override void Behaviour()
    {
        base.Behaviour();
    }
}