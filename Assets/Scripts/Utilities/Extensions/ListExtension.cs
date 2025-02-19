using System.Collections.Generic;
using UnityEngine;
using System;

public class ListExtension: MonoBehaviour
{
    public static List<T> InitializeList<T>(int pAmount)
    {
        int lLength = pAmount;
        List<T> list = new List<T>();
        for(int i = 0 ; i < lLength ; i++)
        {
            list.Add(default(T));
        }
        return list;
    }

    public static List<T> RemoveXpercentOfList<T>(List<T> pList,float pPercentage,bool RemoveEnd = true)
    {
        List<T> list = new List<T>(pList);

        int elementsToRemove = Mathf.FloorToInt(pList.Count * (pPercentage / 100));

        int startIndex = 0;
        int endIndex = list.Count - elementsToRemove;

        if(RemoveEnd)
        {
            startIndex = endIndex;
        }

        // Remove the elements from the list
        list.RemoveRange(startIndex,elementsToRemove);

        return list;
    }

    public static List<T> RemoveXpercentOfList<T>(List<T> pList,float pPercentage,int minIndex,bool RemoveEnd = true)
    {
        List<T> list = new List<T>(pList);

        int elementsToRemove = Mathf.FloorToInt(pList.Count * (pPercentage / 100));

        int startIndex = 0;
        int endIndex = list.Count - elementsToRemove;

        if(RemoveEnd)
        {
            startIndex = endIndex;
        }
        if(minIndex > startIndex)
        {
            startIndex = minIndex + 1;
        }
        // Remove the elements from the list
        list.RemoveRange(startIndex,elementsToRemove);

        return list;
    }

    public static List<Type> TypeList()
    {
        return new List<Type>();
    }

    public static List<T> ArrayToList<T>(T[] array)
    {
        // If the array is null, return an empty list (or you could throw an exception).
        if(array == null)
        {
            return new List<T>();
        }

        // Initialize the list with the capacity of the array for better performance.
        List<T> list = new List<T>(array.Length);
        list.AddRange(array);
        return list;
    }
}