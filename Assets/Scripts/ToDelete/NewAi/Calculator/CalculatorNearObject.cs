using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorNearObject: MonoBehaviour
{
    public float distance;
    private List<GameObject> nearObjects;
    public BoxColliderCreationParam param;

    private void GetNearObjects()
    {
        param.center = transform.position;
        nearObjects = CollisionExtensions.CheckForNearbyObject(param,ShapeType.Box);
    }
}