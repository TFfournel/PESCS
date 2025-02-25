using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearObjectCompute: Computor
{
    public float distance;
    private List<GameObject> nearObjects;
    public BoxColliderCreationParam param;

    public override void compute()
    {
        param.center = transform.position;
        nearObjects = CollisionExtensions.CheckForNearbyObject(param,ShapeType.Box);
    }
}