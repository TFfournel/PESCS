using System.Collections.Generic;
using UnityEngine;

public class DebugExtension: MonoBehaviour
{
    public static void DebugList(List<object> pList)
    {
        foreach(object t in pList)
        {
            Debug.Log(t);
        }
    }

    public static void LineOnPoints(List<Vector3> pPoints)
    {
    }
}