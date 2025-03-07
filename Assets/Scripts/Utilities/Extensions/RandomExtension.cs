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

    /*
    public static Vector3 randomPosInRange(Vector3 pMin,Vector3 pMax)
    {
        Vector3 lResult = RandomExtension.RandomInRange()
        return lResult;
    }*/

    public static List<Vector3> RandomAounrd(Vector3 pCenter,Vector3 pSize,int pAmount)
    {
        int lLength = pAmount;
        List<Vector3> pList = new List<Vector3>();
        for(int i = 0 ; i < lLength ; i++)
        {
            Vector3 lVector = RandomAround(pCenter,pSize);
            pList.Add(lVector);
        }

        return pList;
    }

    public static Vector3 RandomAround(Vector3 pCenter,Vector3 pSize)
    {
        float x = Random.Range(pCenter.x - pSize.x / 2,pCenter.x + pSize.x / 2);
        float y = Random.Range(pCenter.y - pSize.y / 2,pCenter.y + pSize.y / 2);
        float z = Random.Range(pCenter.z - pSize.z / 2,pCenter.z + pSize.z / 2);
        Vector3 lResult = new Vector3(x,y,z);
        return lResult;
    }

    public static int RandomInRange(int pCount)
    {
        int lValue = Random.Range(0,pCount);
        return lValue;
    }
}