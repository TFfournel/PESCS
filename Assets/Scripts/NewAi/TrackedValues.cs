using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrackedValueInfo
{
    public Delegate GetValueMethod;
    public object value;
}

public class TrackedValues: MonoBehaviour
{
    public Dictionary<string,TrackedValueInfo> trackedValues = new Dictionary<string,TrackedValueInfo>();

    public void Awake()
    {
        BehaviourTree lTree = GetXComponent<BehaviourTree>();
        lTree.trackedValues = this;
    }

    public void Start()
    {
        InitializeValues();
    }

    public void InitializeValues()
    {
        GetXComponent<NavMeshAgent>();
        GetXComponent<Pathfinding>();
    }

    public void SetComponent(string pName)
    {
    }

    public T GetXComponent<T>() where T : Component
    {
        string typeKey = typeof(T).ToString();
        T val = null;
        if(trackedValues.ContainsKey(typeKey))
        {
            return trackedValues[typeKey].value as T;  //
        }
        TrackedValueInfo lNewInfo = new TrackedValueInfo
        {
            GetValueMethod = null
        };
        T Component = GetComponent<T>();
        if(Component != null)
        {
            lNewInfo.value = Component;
        }
        else
        {
            lNewInfo.value = gameObject.AddComponent<T>();  //
        }

        trackedValues.Add(typeKey,lNewInfo);
        val = lNewInfo.value as T;
        return val;  //
    }

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
}