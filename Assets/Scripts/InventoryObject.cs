using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject: MonoBehaviour
{
    public static int OBJECT_COUNT;

    public int ID;

    // Start is called before the first frame update
    private void Start()
    {
        ID = OBJECT_COUNT;
        OBJECT_COUNT++;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}