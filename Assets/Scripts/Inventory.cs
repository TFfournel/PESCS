using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPickUpParam
{
    public bool deleteInstance = false;
    public bool parrentToPlayer = true;
}

public class Inventory: MonoBehaviour
{
    /*public Dictionary<string,GameObject> allObjects = new Dictionary<string,GameObject>();

    public void AddObject(InventoryPickUpParam pParam,InventoryObject pObject)
    {
        if (pParam.deleteInstance)
        {
            allObjects.Add(pObject.ID,);
        }
            allObjects.Add(Instantiate(pObject));
        allObjects.Add(pObject);
    }*/
}