using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AiStateGoToPos: AiStateBaseIncrease
{
    private Vector3 targetPos;
    public float randomPosRadius = 50f;
    private float elapsedTime;
    public float speed = 5f;

    public override void OnStateChange()
    {
        base.OnStateChange();
        /*if(onChangeStateParam.active)
        {
            behaviourTree.trackedValues.GetXComponent<Pathfinding>(typeof(Pathfinding)).SetTarget(targetPos);
        }*/
        targetPos = RandomExtension.RandomPointInSphere(transform.position,randomPosRadius);
        Debug.Log("target" + targetPos);
    }

    public override void Behaviour()
    {
        base.Behaviour();
        Vector3 lMovementVector =
            VectorExtensions.Direction(behaviourTree.gameObject.transform.position,targetPos).normalized * speed;
        Debug.Log("movementVector" + lMovementVector);
        behaviourTree.gameObject.transform.position += lMovementVector * Time.deltaTime;
        elapsedTime += Time.deltaTime * speed;
        Debug.Log("move");
    }
}