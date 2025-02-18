using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootMousePos: MonoBehaviour
{
    public Weapon currentWeaponprefab;
    [HideInInspector] public Weapon currentWeaponInstance;

    private void Start()
    {
        currentWeaponInstance = Instantiate(currentWeaponprefab.gameObject).GetComponent<Weapon>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            SetDirection(Input.mousePosition);
            currentWeaponInstance.ShootRequest();
            Debug.Log("change direction");
        }
    }

    private void SetDirection(Vector3 pTargetPos)
    {
        Vector3 lDirection = VectorExtensions.Direction(transform.position,pTargetPos);
        transform.rotation = Quaternion.LookRotation(lDirection,transform.up);
    }
}