using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomExtension: MonoBehaviour
{
    public static Vector3 RandomPointInSphere(Vector3 pPos,float pRadius)
    {
        Vector3 lGeneratedPos = pPos + Random.insideUnitSphere * pRadius;
        return lGeneratedPos;
    }

    public static void MeshPickRandomVertex()
    {
    }
}