using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShootMousePos: MonoBehaviour
{
    public Weapon currentWeaponprefab;
    [HideInInspector] public Weapon currentWeaponInstance;
    public bool directionToForward = false;
    public AiValues AiValues;

    public MouseButton shootOnTargetKey = MouseButton.Left;
    private Vector3 target;
    private Ray mouseCollisionRay;
    private RaycastHit raycastHit;

    private void Start()
    {
        currentWeaponInstance = Instantiate(currentWeaponprefab.gameObject,transform.position,transform.rotation,transform).GetComponent<Weapon>();
        AiValues.weapon = currentWeaponInstance;
    }

    // Update is called once per frame
    private void Update()
    {
        ShootOnClick();
    }

    private void SetDirection(Vector3 pTargetPos)
    {
        Vector3 lDirection = VectorExtensions.Direction(transform.position,pTargetPos);
        if(directionToForward)
        {
            lDirection = transform.forward;
        }

        transform.rotation = Quaternion.LookRotation(lDirection,transform.up);
    }

    private void ShootOnClick()
    {
        if(Input.GetMouseButtonDown((int)shootOnTargetKey))
        {
            mouseCollisionRay = CollisionExtensions.MouseCameraRaycast();
            raycastHit = CollisionExtensions.GetRayData(mouseCollisionRay);
            GameObject lObjectHit = raycastHit.transform.gameObject;
            if(lObjectHit is null)
                return;
            target = raycastHit.point;
            SetDirection(target);
            AiValues.weapon.ShootRequest();
        }
    }
}