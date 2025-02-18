using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToPickUp: MonoBehaviour
{
    public Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }

    public GameObject PickUpObject(Transform pParent)
    {
        GameObject lGameObject = gameObject;
        transform.SetParent(pParent);
        return lGameObject;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}