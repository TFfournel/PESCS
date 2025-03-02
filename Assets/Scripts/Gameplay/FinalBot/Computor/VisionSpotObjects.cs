using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionSpotObjects: MonoBehaviour
{
    public List<Type> allTypeToSearch = new List<Type>();
    private Dictionary<Type,object> allType = new Dictionary<Type,object>();

    /*public void GetAllClasses()
    {
        int lLength = allTypeToSearch.Count;
        List<object> allstuff;
        for(int i = 0 ; i < lLength ; i++)
        {
            allstuff = ListExtension.LookForType<AiStateManager>(aiStateManager.aiValues.nearbyObjects);
            allEnemies =
            nearEnemies = allEnemies.Count;
        }
    }*/
}