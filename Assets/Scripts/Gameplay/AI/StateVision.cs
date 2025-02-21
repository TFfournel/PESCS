/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StateVision: AiState
{
    public Vector3 size;
    public Vector3 offset;
    public bool startFromBorder = true;
    private Vector3 startPos;
    private Collider collider;

    protected override void Init(AiStateParameter pParam)
    {
        base.Init(pParam);
    }

    public override void Factor()
    {
        base.Factor();
    }

    protected override void SetChangeState()
    {
        base.SetChangeState();
        startPos = transform.position + transform.rotation * offset;
        if(startFromBorder)
            startPos += transform.rotation * VectorExtensions.Multiply(size / 2,new Vector3(1,0,1));
        collider = CollisionExtensions.CreateCubeCollider(transform.position + transform.rotation * offset,gameObject,size);
    }

    public override void Behaviour()
    {
        base.Behaviour();
    }
}*/