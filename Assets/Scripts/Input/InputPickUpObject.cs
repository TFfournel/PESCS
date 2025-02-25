using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPickUpObject: MonoBehaviour
{
    public KeyCode keyToPickup = KeyCode.F;
    public Vector3 boxSize;
    private Collider collider;
    public Vector3 offset = Vector3.forward;
    public float offsetDistanceMultiplier;

    private void Start()
    {
    }

    private void OnTriggerStay(Collider pCollider)
    {
        ObjectToPickUp lObjectToPickUp = pCollider.GetComponent<ObjectToPickUp>();
        if(lObjectToPickUp is null)
            return;
        if(Input.GetKeyDown(keyToPickup))
        {
            lObjectToPickUp.PickUpObject(transform);
            lObjectToPickUp.transform.position = transform.position + offset * offsetDistanceMultiplier;
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}