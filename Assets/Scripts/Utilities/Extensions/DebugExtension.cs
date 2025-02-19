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

    /*
    private void DebugPathRay()
    {
        Vector3[] lPath = agent.path.corners;
        if(lPath == null || lPath.Length < 2)
        {
            return;
        }

        for(int i = 0 ; i < lPath.Length - 1 ; i++)
        {
            Debug.DrawLine(lPath[i],lPath[i + 1],Color.green);
        }
    }*/
}