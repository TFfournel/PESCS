using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootMousePos: MonoBehaviour
{
    private Weapon currentWeapon;

    private void Start()
    {
        currentWeapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            SetDirection(Input.mousePosition);
            currentWeapon.ShootRequest();
        }
    }

    private void SetDirection(Vector3 pTargetPos)
    {
        Vector3 lDirection = VectorExtensions.Direction(transform.position,pTargetPos);
        transform.rotation = Quaternion.LookRotation(lDirection,transform.up);
    }
}