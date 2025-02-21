using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorVision: Calculator
{
    private List<Collider> visionCollider;
    private List<GameObject> spotedObjects = new List<GameObject>();
    public BoxColliderCreationParam pColliderCreationParam;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Compute()
    {
        base.Compute();
        startPos = transform.position + transform.rotation * offset;
        if(startFromBorder)
            startPos += transform.rotation * VectorExtensions.Multiply(pColliderCreationParam.size / 2,new Vector3(1,0,1));
        pColliderCreationParam.center = startPos;
        visionCollider = CollisionExtensions.GetOverlap(pColliderCreationParam);
        spotedObjects.Clear();
        spotedObjects = ListExtension.ExecuteOnEach(visionCollider,hit => hit.gameObject);

        spotedObjects = RaycastCheck(spotedObjects);
        DebugExtension.DrawBox(pColliderCreationParam.center,pColliderCreationParam.size,Color.gray,updateFrequency);
    }

    private List<GameObject> RaycastCheck(List<GameObject> objectToCheck)
    {
        List<GameObject> raycastedObjects = new List<GameObject>();
        List<RaycastHit> lRaycastHits =
            CollisionExtensions.RaycastToObjects(transform.position,objectToCheck);
        List<GameObject> lObject = ListExtension.ExecuteOnEach(lRaycastHits,hit => hit.collider.gameObject);
        raycastedObjects = ListExtension.KeepOnlyCommon<GameObject>(objectToCheck,lObject);
        foreach(GameObject VARIABLE in raycastedObjects)
        {
            DebugExtension.DrawLine(transform.position,VARIABLE.transform.position,Color.red,updateFrequency);
        }
        return raycastedObjects;
    }

    public Vector3 offset;
    public bool startFromBorder = true;
    private Vector3 startPos;
    private Collider collider;
}