using System;
using System.Collections.Generic;
using UnityEngine;

public class TrackedValueInfo
{
    public Delegate GetValueMethod;
    public object value;
}

public class TrackedValues: MonoBehaviour
{
    public Dictionary<string,TrackedValueInfo> trackedValues = new Dictionary<string,TrackedValueInfo>();

    public T CheckForTrackValues<T>(string pValueName)
    {
        if(!trackedValues.ContainsKey(pValueName))
            return default;

        TrackedValueInfo lThing = trackedValues[pValueName];

        if(lThing.value is T typedValue) //
        {
            return typedValue;
        }

        return default;
    }

    public T GetXComponent<T>() where T : Component
    {
        string typeKey = typeof(T).ToString();

        if(trackedValues.ContainsKey(typeKey))
        {
            return trackedValues[typeKey].value as T;  //
        }

        TrackedValueInfo lNewInfo = new TrackedValueInfo
        {
            GetValueMethod = null
        };

        lNewInfo.value = gameObject.AddComponent<T>();  //

        trackedValues.Add(typeKey,lNewInfo);

        return lNewInfo.value as T;  //
    }
}