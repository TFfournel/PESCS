using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionExtensions: MonoBehaviour
{
    /*public static Collider CreateCubeCollider(Vector3 pPos,GameObject pObject,Vector3 pSize)

    {
    }

    public static Collider CreateSphereCollider(Vector3 pPos,GameObject pObject,Vector3 pSize)
    {
    }

    public static ColliderHit HitMousePos(Vector3 pStartPosition,)
    {
    }*/

    public static Ray MouseCameraRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray;
    }

    public static RaycastHit GetRayData(Ray pRay)
    {
        RaycastHit lRaycastHitData;
        Physics.Raycast(pRay,out lRaycastHitData);
        return lRaycastHitData;
    }
}