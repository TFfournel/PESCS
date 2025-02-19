using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;
using UnityEngine.AI;

public class AiValues: MonoBehaviour
{
    public float health;
    public static List<AiValues> allies = new List<AiValues>();

    public Weapon weapon;
    public Bot bot;
    public BoxColliderCreationParam pParam;
    public List<GameObject> nearbyObjects = new List<GameObject>();
    public Pathfinding pathfinding;

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
        //CheckNearObjects();
    }

    private void LateUpdate()
    {
        pParam.center = transform.position;
        nearbyObjects = CollisionExtensions.CheckForNearbyObject(pParam,ShapeType.Box);
    }

    private void OnDestroy()
    {
        allies.Remove(this);
    }
}