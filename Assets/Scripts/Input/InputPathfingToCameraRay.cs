using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathfindingToCameraRayInput: MonoBehaviour
{
    public Pathfinding pathfinding;
    public MouseButton moveToTargetKey = MouseButton.Right;
    private Vector3 target;
    private Ray mouseCollisionRay;
    private RaycastHit raycastHit;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown((int)moveToTargetKey))
        {
            mouseCollisionRay = CollisionExtensions.MouseCameraRaycast();
            raycastHit = CollisionExtensions.GetRayData(mouseCollisionRay);
            GameObject lObjectHit = raycastHit.transform.gameObject;
            if(lObjectHit is null)
                return;
            target = raycastHit.point;
            pathfinding.SetTarget(target);
        }
    }
}