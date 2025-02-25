using System;
using System.Collections.Generic;
using System.Reflection;
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
    public Weapon weapon;
    public CalculatorVision calculatorVision;

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

    public void Update()
    {
        DebugExtension.DebugDictionary(trackedValues);
    }

    /// <summary>
    /// Retrieves (or adds) a component of type T on this GameObject and tracks it.
    /// </summary>
    public T GetXComponent<T>() where T : Component
    {
        string typeKey = typeof(T).ToString();
        if(trackedValues.ContainsKey(typeKey))
        {
            return trackedValues[typeKey].value as T;
        }
        TrackedValueInfo newInfo = new TrackedValueInfo { GetValueMethod = null };
        T comp = GetComponent<T>();
        if(comp != null)
        {
            newInfo.value = comp;
        }
        else
        {
            newInfo.value = gameObject.AddComponent<T>();
        }
        trackedValues.Add(typeKey,newInfo);
        return newInfo.value as T;
    }

    /// <summary>
    /// Overload: Retrieves (or adds) a component of type T on this GameObject, tracks it,
    /// and copies all serializable data from the provided source instance.
    /// </summary>
    public T GetXComponent<T>(T source) where T : Component
    {
        if(source == null)
        {
            Debug.LogWarning("Source component is null. Returning default component.");
            return GetXComponent<T>();
        }
        // Get or add the tracked component.
        T target = GetXComponent<T>();
        // Copy the values from the source instance into the target.
        CopyComponent(source,target);
        return target;
    }

    /// <summary>
    /// Retrieves (or adds) a component based on a type name provided as a string.
    /// It uses the same logic as the generic version but accepts a type name,
    /// then uses a TrackedValueInfo to track the component.
    /// </summary>
    public Component GetXComponent(string typeName)
    {
        // Check if we already have a tracked value for this type name.
        if(trackedValues.ContainsKey(typeName))
        {
            return trackedValues[typeName].value as Component;
        }

        // Attempt to resolve the type.
        Type type = Type.GetType(typeName);
        if(type == null)
        {
            Debug.LogError("Type " + typeName + " could not be found. Ensure it is fully qualified.");
            return null;
        }

        // Create a new TrackedValueInfo instance.
        TrackedValueInfo newInfo = new TrackedValueInfo { GetValueMethod = null };
        Component comp = GetComponent(type);
        if(comp != null)
        {
            newInfo.value = comp;
        }
        else
        {
            newInfo.value = gameObject.AddComponent(type);
        }
        trackedValues.Add(typeName,newInfo);
        return newInfo.value as Component;
    }

    /// <summary>
    /// Returns the tracked value of type T for the given key.
    /// </summary>
    public T CheckForTrackValues<T>(string pValueName)
    {
        if(!trackedValues.ContainsKey(pValueName))
            return default;

        TrackedValueInfo info = trackedValues[pValueName];
        if(info.value is T typedValue)
        {
            return typedValue;
        }
        return default;
    }

    /// <summary>
    /// Copies fields and writable properties from the source component to the destination component.
    /// This performs a shallow copy using reflection.
    /// </summary>
    private void CopyComponent<T>(T source,T destination) where T : Component
    {
        if(source == null || destination == null)
            return;

        Type type = source.GetType();

        // Copy all fields (public and private).
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach(FieldInfo field in fields)
        {
            field.SetValue(destination,field.GetValue(source));
        }

        // Copy properties that are both readable and writable.
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        foreach(PropertyInfo prop in properties)
        {
            if(prop.CanWrite && prop.CanRead)
            {
                try
                {
                    object value = prop.GetValue(source,null);
                    prop.SetValue(destination,value,null);
                }
                catch
                {
                    // Some properties might not be supported; skip them.
                }
            }
        }
    }
}