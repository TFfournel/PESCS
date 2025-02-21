using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AiStateGoToPos: AiStateBaseIncrease
{
    private Vector3 targetPos;
    public float randomPosRadius = 50f;
    public float speed = 5f;

    public override void OnStateChange()
    {
        base.OnStateChange();
        /*if(onChangeStateParam.active)
        {
            behaviourTree.trackedValues.GetXComponent<Pathfinding>(typeof(Pathfinding)).SetTarget(targetPos);
        }*/
        targetPos = RandomExtension.RandomPointInSphere(transform.position,randomPosRadius);
        Debug.Log("target pos" + targetPos);

        Pathfinding lPath = behaviourTree.trackedValues.GetXComponent<Pathfinding>();
        targetPos = NavMeshExtensions.ClosestNavMeshPos(targetPos,randomPosRadius).position;
        lPath.SetTarget(targetPos);
    }

    public override void Behaviour()
    {
        base.Behaviour();
    }
}