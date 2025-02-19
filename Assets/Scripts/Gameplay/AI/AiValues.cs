using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;

public class AiValues: MonoBehaviour
{
    public float health;
    public static List<AiValues> allies = new List<AiValues>();

    public Weapon weapon;
    public Bot bot;
    public BoxColliderCreationParam pParam;
    private List<GameObject> nearbyObjects = new List<GameObject>();

    public List<Type> TypeToCheck = new List<Type>()
    {
        typeof(Weapon),typeof(AiValues)
    };

    // Start is called before the first frame update
    private void Start()
    {
        allies.Add(this);
        bot = GetComponentInChildren<Bot>();
        // weapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckNearObjects();
    }

    /*
    private object GetClass(Type lType,List<object> lList)
    {
    }*/

    private void CheckNearObjects()
    {
        nearbyObjects = CollisionExtensions.CheckForNearbyObject(pParam,ShapeType.Box);
        List<List<object>> allDetectedObject = new List<List<object>>();
    }

    private List<List<object>> allClassChecked(List<GameObject> allGameObject)
    {
        List<object> allDetectedObject = new List<object>();
        List<List<object>> allDetectedClass = new List<List<object>>();
        int lLength = allGameObject.Count;
        int lTypeCheckLength = TypeToCheck.Count;
        object lObject;
        Type lType;
        GameObject lGameObject;
        int lInitializeList = TypeToCheck.Count;
        /*for(int i = 0 ; i < lInitializeList ; i++)
        {
            lType = TypeToCheck[i];
List<object> lList  = new List<object>();
allDetectedClass.Add(ListExtension.InitializeList<>());
        }*/
        for(int i = 0 ; i < lLength ; i++)
        {
            allDetectedClass.Add(new List<object>());
            lGameObject = allGameObject[i];
            for(int j = 0 ; j < lTypeCheckLength ; j++)
            {
                lType = TypeToCheck[j];
                lObject = lGameObject.GetComponent(lType);
                if(lObject == null)
                    continue;
                allDetectedClass[i].Add(lObject);
            }
        }
        return allDetectedClass;
    }

    private void OnDestroy()
    {
        allies.Remove(this);
    }
}